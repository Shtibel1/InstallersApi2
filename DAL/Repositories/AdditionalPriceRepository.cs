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
    public class AdditionalPriceRepository : IAdditionalPriceRepository
    {

        private readonly ICompanyDataProvider _companyDataProvider;

        public AdditionalPriceRepository(ICompanyDataProvider companyDataProvider)
        {
            _companyDataProvider = companyDataProvider;
        }
        public async Task<List<AdditionalPrice>> CreateAsync(List<AdditionalPrice> AdditionalsPrices)
        {
            var context = _companyDataProvider.GetContexts()[0];
            await context.AdditionalsPrices.AddRangeAsync(AdditionalsPrices).ConfigureAwait(false);
            await context.SaveChangesAsync();
            return AdditionalsPrices;
        }

        // Delete an AdditionalPrice entity by Id
        public async Task DeleteAsync(Guid id)
        {
            var context = _companyDataProvider.GetContexts()[0];
            var additionalPrice = await context.AdditionalsPrices.FindAsync(id);
            if (additionalPrice == null)
            {
                throw new Exception("AdditionalPrice not found");
            }
            context.AdditionalsPrices.Remove(additionalPrice);
            await context.SaveChangesAsync();
        }

        // Get an AdditionalPrice by ServiceProviderId and ProductId
        public async Task<AdditionalPrice> Get(Guid serviceProviderId, Guid productId)
        {
            var context = _companyDataProvider.GetContexts()[0];
            return await context.AdditionalsPrices
                                .FirstOrDefaultAsync(ap => ap.ServiceProviderIdExt == serviceProviderId && ap.ProductId == productId);
        }

        public async Task<List<AdditionalPrice>> GetBySP(Guid serviceProviderId)
        {
            var context = _companyDataProvider.GetContexts()[0];

            var latestPrices = await context.AdditionalsPrices
                .Where(ap => ap.ServiceProviderIdExt == serviceProviderId)
                .GroupBy(ap => new { ap.ProductId, ap.AdditionalId }) // Group by ProductId and AdditionalId
                .Select(g => g.OrderByDescending(ap => ap.CreatedDate).FirstOrDefault()) // Get the latest entry in each group
                .ToListAsync();

            return latestPrices;
        }

        // Update a list of AdditionalPrice entities
        public async Task<List<AdditionalPrice>> UpdateAsync(List<AdditionalPrice> AdditionalsPrices)
        {
            var context = _companyDataProvider.GetContexts()[0];

            context.AdditionalsPrices.UpdateRange(AdditionalsPrices);
            await context.SaveChangesAsync();
            return AdditionalsPrices;
        }
    }
}
