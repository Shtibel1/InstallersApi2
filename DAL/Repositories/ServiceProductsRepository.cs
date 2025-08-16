using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ServiceProductsRepository : IServiceProductsRepository
    {
        private readonly ICompanyDataProvider _companyDataProvider;
        private CompanyDbContext _context => _companyDataProvider.GetContexts()[0];

        public ServiceProductsRepository(ICompanyDataProvider companyDataProvider)
        {
            _companyDataProvider = companyDataProvider;

        }
        public Task<List<ServiceProduct>> ListServiceProductsAsync(CancellationToken ct = default) =>
        _context.ServiceProducts.AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct);

        public Task<ServiceProduct?> GetServiceProductAsync(Guid id, CancellationToken ct = default) =>
            _context.ServiceProducts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

        public async Task<ServiceProduct> CreateServiceProductAsync(ServiceProduct sp, CancellationToken ct = default)
        {
            if (sp.Id == Guid.Empty) sp.Id = Guid.NewGuid();
            if (string.IsNullOrWhiteSpace(sp.Name)) throw new ArgumentException("Name is required.", nameof(sp));

            await _context.ServiceProducts.AddAsync(sp, ct);
            await _context.SaveChangesAsync(ct);
            return sp;
        }

        public async Task UpdateServiceProductAsync(ServiceProduct sp, CancellationToken ct = default)
        {
            if (sp.Id == Guid.Empty) throw new ArgumentException("Id is required.", nameof(sp));
            _context.ServiceProducts.Update(sp);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteServiceProductAsync(Guid id, CancellationToken ct = default)
        {
            // Protect from FK violations (Stock/Audit are NO ACTION)
            bool hasDeps =
                await _context.ProductRequiredServiceProducts.AnyAsync(x => x.ServiceProductId == id, ct) ||
                await _context.ServiceProviderStock.AnyAsync(x => x.ServiceProductId == id, ct) ||
                await _context.ServiceProviderStockAudit.AnyAsync(x => x.ServiceProductId == id, ct);

            if (hasDeps)
                throw new InvalidOperationException("Cannot delete: service product is referenced by requirements or stock/audit.");

            _context.ServiceProducts.Remove(new ServiceProduct { Id = id });
            await _context.SaveChangesAsync(ct);
        }

        // ===== Requirements per product ==========================================

        public Task<IReadOnlyList<ProductRequiredServiceProduct>> GetRequirementsAsync(Guid productId, CancellationToken ct = default) =>
            _context.ProductRequiredServiceProducts
                .AsNoTracking()
                .Where(x => x.ProductId == productId)
                .Include(x => x.ServiceProduct)
                .OrderBy(x => x.ServiceProduct.Name)
                .ToListAsync(ct)
            .ContinueWith<IReadOnlyList<ProductRequiredServiceProduct>>(t => t.Result, ct);

        public async Task UpsertRequirementAsync(Guid productId, Guid serviceProductId, int quantity, CancellationToken ct = default)
        {
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            var existing = await _context.ProductRequiredServiceProducts
                .FirstOrDefaultAsync(x => x.ProductId == productId && x.ServiceProductId == serviceProductId, ct);

            if (existing is null)
            {
                _context.ProductRequiredServiceProducts.Add(new ProductRequiredServiceProduct
                {
                    ProductId = productId,
                    ServiceProductId = serviceProductId,
                    Quantity = quantity
                });
            }
            else
            {
                existing.Quantity = quantity;
            }

            await _context.SaveChangesAsync(ct);
        }

        public async Task SetAllRequirementsAsync(Guid productId, IEnumerable<ProductRequirement> requirements, CancellationToken ct = default)
        {
            var reqList = requirements?.ToList() ?? new();
            if (reqList.Any(r => r.Quantity <= 0)) throw new ArgumentException("All quantities must be > 0.", nameof(requirements));

            var existing = await _context.ProductRequiredServiceProducts
                .Where(x => x.ProductId == productId)
                .ToListAsync(ct);

            var toKeep = reqList.Select(r => r.ServiceProductId).ToHashSet();

            // Remove missing
            _context.ProductRequiredServiceProducts.RemoveRange(existing.Where(e => !toKeep.Contains(e.ServiceProductId)));

            // Upsert remaining
            foreach (var r in reqList)
            {
                var row = existing.FirstOrDefault(e => e.ServiceProductId == r.ServiceProductId);
                if (row is null)
                {
                    _context.ProductRequiredServiceProducts.Add(new ProductRequiredServiceProduct
                    {
                        ProductId = productId,
                        ServiceProductId = r.ServiceProductId,
                        Quantity = r.Quantity
                    });
                }
                else
                {
                    row.Quantity = r.Quantity;
                }
            }

            await _context.SaveChangesAsync(ct);
        }

        public async Task RemoveRequirementAsync(Guid productId, Guid serviceProductId, CancellationToken ct = default)
        {
            var row = await _context.ProductRequiredServiceProducts
                .FirstOrDefaultAsync(x => x.ProductId == productId && x.ServiceProductId == serviceProductId, ct);

            if (row is not null)
            {
                _context.ProductRequiredServiceProducts.Remove(row);
                await _context.SaveChangesAsync(ct);
            }
        }

        // ===== Stock per provider + audit ========================================

        public async Task<int> GetStockAmountAsync(Guid providerId, Guid serviceProductId, CancellationToken ct = default)
        {
            var amount = await _context.ServiceProviderStock
                .Where(s => s.ServiceProviderIdExternal == providerId && s.ServiceProductId == serviceProductId)
                .Select(s => (int?)s.Amount)
                .FirstOrDefaultAsync(ct);
            return amount ?? 0;
        }

        public Task<IReadOnlyList<ServiceProviderStock>> GetProviderStockAsync(Guid providerId, CancellationToken ct = default) =>
            _context.ServiceProviderStock
                .AsNoTracking()
                .Where(s => s.ServiceProviderIdExternal == providerId)
                .Include(s => s.ServiceProduct)
                .OrderBy(s => s.ServiceProduct.Name)
                .ToListAsync(ct)
            .ContinueWith<IReadOnlyList<ServiceProviderStock>>(t => t.Result, ct);

        public Task<List<ServiceProviderStockAudit>> GetServiceProviderStockAuditsAsync(Guid providerId, Guid serviceProductId, CancellationToken ct = default) =>
              _context.ServiceProviderStockAudit
                .AsNoTracking()
                .Where(s => s.ServiceProviderIdExternal == providerId && s.ServiceProductId == serviceProductId)
                .Take(500)
                .OrderByDescending(s => s.PerformedAt)
                .ToListAsync(ct);

        public async Task AdjustStockAsync(Guid providerId, Guid serviceProductId, int delta,
                                           Guid? performedByUserId = null, Guid? referenceId = null, string? reason = null,
                                           CancellationToken ct = default)
        {
            if (delta == 0) return;

            var stock = await _context.ServiceProviderStock
                .SingleOrDefaultAsync(s => s.ServiceProviderIdExternal == providerId && s.ServiceProductId == serviceProductId, ct);

            if (stock is null)
            {
                stock = new ServiceProviderStock
                {
                    Id = Guid.NewGuid(),
                    ServiceProviderIdExternal = providerId,
                    ServiceProductId = serviceProductId,
                    Amount = 0
                };
                _context.ServiceProviderStock.Add(stock);
            }

            var newAmount = stock.Amount + delta;
            if (newAmount < 0) throw new InvalidOperationException("Insufficient stock.");

            stock.Amount = newAmount;

            _context.ServiceProviderStockAudit.Add(new ServiceProviderStockAudit
            {
                Id = Guid.NewGuid(),
                ServiceProviderIdExternal = providerId,
                ServiceProductId = serviceProductId,
                Delta = delta,
                BalanceAfter = newAmount,
                Reason = reason,
                PerformedByUserId = performedByUserId,
                ReferenceId = referenceId,
                PerformedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync(ct);
        }

        // ===== Helper: shortages for installing a product by a provider ===========
        public async Task<IReadOnlyList<(Guid ServiceProductId, int Required, int Have, int Missing)>> GetShortagesAsync(
            Guid providerId, Guid productId, CancellationToken ct = default)
        {
            var shortages = await (
                from req in _context.ProductRequiredServiceProducts.AsNoTracking()
                    .Where(r => r.ProductId == productId)
                join s in _context.ServiceProviderStock.AsNoTracking()
                    .Where(x => x.ServiceProviderIdExternal == providerId)
                    on req.ServiceProductId equals s.ServiceProductId into g
                from s in g.DefaultIfEmpty()
                let have = s != null ? s.Amount : 0
                where have < req.Quantity
                select new { req.ServiceProductId, Required = req.Quantity, Have = have, Missing = req.Quantity - have }
            ).ToListAsync(ct);

            return shortages.Select(x => (x.ServiceProductId, x.Required, x.Have, x.Missing)).ToList();
        }
    }
}
