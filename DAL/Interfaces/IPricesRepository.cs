using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IServiceProviderPricingRepository 
    {
        Task<List<ServiceProviderPricing>> GetInstallerPricingByInstallerAsync(Guid installerId);
        Task<ServiceProviderPricing> UpdateInstallerPricingAsync(Guid installerId, ServiceProviderPricing InstallerPricing);
        /*Task<InstallerPricing> CreateInstallerPricingAsync(InstallerPricing InstallerPricing);*/
        Task DeleteInstallerPricingAsync(Guid installerId);

        Task<ServiceProviderPricing> GetServiceProviderPicing(Guid installerId, Guid productId);
        Task<List<ServiceProviderPricing>> PricesComprasion(List<Guid> ServiceProviderIds, Guid productId);

    }
}
