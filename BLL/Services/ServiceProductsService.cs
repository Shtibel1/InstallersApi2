using AutoMapper;
using BLL.Interfaces;
using BLL.Vms;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceProductsService : IServiceProductsService
    {
        private readonly IServiceProductsRepository _repo;
        private readonly IMapper _mapper;

        public ServiceProductsService(IServiceProductsRepository serviceProductsRepository, IMapper mapper)
        {
            _repo = serviceProductsRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<ServiceProductVm>> ListServiceProductsAsync(CancellationToken ct = default)
        {
            var entities = await _repo.ListServiceProductsAsync(ct);
            return _mapper.Map<IReadOnlyList<ServiceProductVm>>(entities);
        }

        public async Task<ServiceProductVm?> GetServiceProductAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _repo.GetServiceProductAsync(id, ct);
            return _mapper.Map<ServiceProductVm?>(entity);
        }

        public async Task<ServiceProductVm> CreateServiceProductAsync(ServiceProductVm vm, CancellationToken ct = default)
        {
            var name = NormalizeName(vm?.Name);
            ValidateName(name);

            var entity = _mapper.Map<ServiceProduct>(vm);
            entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
            entity.Name = name;

            var created = await _repo.CreateServiceProductAsync(entity, ct);
            return _mapper.Map<ServiceProductVm>(created);
        }

        public async Task UpdateServiceProductAsync(ServiceProductVm vm, CancellationToken ct = default)
        {
            if (vm == null || vm.Id == Guid.Empty) throw new ArgumentException("Valid Id is required.", nameof(vm));

            var existing = await _repo.GetServiceProductAsync((Guid)vm.Id, ct);
            if (existing == null) throw new KeyNotFoundException("Service product not found.");

            var name = NormalizeName(vm.Name);
            ValidateName(name);

            var entity = _mapper.Map<ServiceProduct>(vm);
            entity.Name = name;

            await _repo.UpdateServiceProductAsync(entity, ct);
        }

        public Task DeleteServiceProductAsync(Guid id, CancellationToken ct = default) =>
            _repo.DeleteServiceProductAsync(id, ct);

        // -------- Requirements per product --------

        public async Task<IReadOnlyList<ProductRequirementVm>> GetRequirementsAsync(Guid productId, CancellationToken ct = default)
        {
            var rows = await _repo.GetRequirementsAsync(productId, ct);
            return rows.Select(r => new ProductRequirementVm
            {
                ServiceProductId = r.ServiceProductId,
                ServiceProductName = r.ServiceProduct?.Name,
                Quantity = r.Quantity
            }).ToList();
        }

        public Task UpsertRequirementAsync(Guid productId, Guid serviceProductId, int quantity, CancellationToken ct = default)
        {
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            return _repo.UpsertRequirementAsync(productId, serviceProductId, quantity, ct);
        }

        public Task SetAllRequirementsAsync(Guid productId, IEnumerable<ProductRequirementVm> requirements, CancellationToken ct = default)
        {
            var list = (requirements ?? Enumerable.Empty<ProductRequirementVm>()).ToList();
            if (list.Any(r => r.Quantity <= 0)) throw new ArgumentException("All quantities must be > 0.", nameof(requirements));

            var reqs = list.Select(r => new ProductRequirement(r.ServiceProductId, r.Quantity));
            return _repo.SetAllRequirementsAsync(productId, reqs, ct);
        }

        public Task RemoveRequirementAsync(Guid productId, Guid serviceProductId, CancellationToken ct = default) =>
            _repo.RemoveRequirementAsync(productId, serviceProductId, ct);

        // -------- Stock per provider (+ audit) --------

        public Task<int> GetStockAmountAsync(Guid providerId, Guid serviceProductId, CancellationToken ct = default) =>
            _repo.GetStockAmountAsync(providerId, serviceProductId, ct);

        public async Task<IReadOnlyList<ServiceProviderStockVm>> GetProviderStockAsync(Guid providerId, CancellationToken ct = default)
        {
            var rows = await _repo.GetProviderStockAsync(providerId, ct);
            var vms = rows.Select(s => new ServiceProviderStockVm
            {
                Id = s.Id,
                ServiceProviderIdExternal = s.ServiceProviderIdExternal,
                ServiceProductId = s.ServiceProductId,
                ServiceProductName = s.ServiceProduct?.Name,
                Amount = s.Amount
            }).ToList();

            foreach (var vm in vms)
            {
                var audit = await _repo.GetServiceProviderStockAuditsAsync(vm.ServiceProviderIdExternal, vm.ServiceProductId, ct);
                vm.AuditVm = audit
                    .Where(a => a.ServiceProviderIdExternal == vm.ServiceProviderIdExternal && a.ServiceProductId == vm.ServiceProductId)
                    .Select(a => _mapper.Map<ServiceProviderStockAuditVm>(a)).ToList();
            }

            return vms;

        }

        public async Task AdjustStockAsync(StockAdjustmentVm adj, CancellationToken ct = default)
        {
            if (adj is null) throw new ArgumentNullException(nameof(adj));
            if (adj.Delta == 0) return;

            await _repo.AdjustStockAsync(
                adj.ServiceProviderIdExternal,
                adj.ServiceProductId,
                adj.Delta,
                adj.PerformedByUserId,
                adj.ReferenceId,
                adj.Reason,
                ct);
        }

        // -------- Helper --------

        public async Task<IReadOnlyList<ShortageVm>> GetShortagesAsync(Guid providerId, Guid productId, bool includeNames = false, CancellationToken ct = default)
        {
            var shortages = await _repo.GetShortagesAsync(providerId, productId, ct);
            var result = shortages.Select(x => new ShortageVm
            {
                ServiceProductId = x.ServiceProductId,
                Required = x.Required,
                Have = x.Have,
                Missing = x.Missing
            }).ToList();

            if (includeNames && result.Count > 0)
            {
                // naive enrichment: fetch all products once and map names in-memory
                var allSps = await _repo.ListServiceProductsAsync(ct);
                var dict = allSps.ToDictionary(sp => sp.Id, sp => sp.Name); // Guid → string
                foreach (var r in result)
                    if (dict.TryGetValue(r.ServiceProductId, out var name))
                        r.ServiceProductName = name;
            }

            return result;
        }

        // ---- helpers ----
        private static string NormalizeName(string? name) => name?.Trim() ?? string.Empty;

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");
            if (name.Length > 200)
                throw new ArgumentException("Name must be <= 200 characters.");
        }
    }
}
