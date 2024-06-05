using DAL.Data;
using DAL.Entities;
using DAL.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ServiceProviderPricingRepository : IServiceProviderPricingRepository
    {
        private readonly ICompanyDataProvider _companyDataProvider;

        public ServiceProviderPricingRepository(ICompanyDataProvider companyDataProvider)
        {
            _companyDataProvider = companyDataProvider;

        }


        public async Task<List<ServiceProviderPricing>> GetInstallerPricingByInstallerAsync(Guid installerId)
        {
            var context = _companyDataProvider.GetContexts()[0];
            return await context.ServiceProviderPricing
                .Where(p => p.InstallerId == installerId)
                .ToListAsync();
        }

        public async Task<ServiceProviderPricing> UpdateInstallerPricingAsync(Guid installerId, ServiceProviderPricing InstallerPricing)
        {
            var context = _companyDataProvider.GetContexts()[0];
            InstallerPricing.InstallerId = installerId;
            var updatedPrice = await context.ServiceProviderPricing.AddAsync(InstallerPricing);
            await context.SaveChangesAsync();
            return updatedPrice.Entity;
        }

        public async Task DeleteInstallerPricingAsync(Guid installerId)
        {
            var context = _companyDataProvider.GetContexts()[0];
            var installerInstallerPricing = await context.ServiceProviderPricing.Where(p => p.InstallerId == installerId).ToListAsync();
            if (installerInstallerPricing != null)
            {
                for (int i = 0; i < installerInstallerPricing.Count; i++)
                {
                    context.Remove(installerInstallerPricing[i]);
                }
            
                await context.SaveChangesAsync();

            }
        }


        public async Task<ServiceProviderPricing> GetServiceProviderPicing(Guid installerId, Guid productId)
        {
            var context = _companyDataProvider.GetContexts()[0];
            return await context.ServiceProviderPricing.FirstOrDefaultAsync(i => i.ProductId == productId && i.InstallerId == installerId);
        }

        public async Task<List<ServiceProviderPricing>> PricesComprasion(List<Guid> ServiceProviderIds, Guid productId)
        {
            var context = _companyDataProvider.GetContexts()[0];
            return await context.ServiceProviderPricing
                .Where(p => ServiceProviderIds.Contains(p.InstallerId) && p.ProductId == productId && p.InstallationPrice > 0)
                .ToListAsync();
        }
    }
}
    