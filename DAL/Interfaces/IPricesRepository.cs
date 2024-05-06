using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IInstallerPricingRepository 
    {
        Task<List<InstallerPricing>> GetInstallerPricingByInstallerAsync(Guid installerId);
        Task<InstallerPricing> UpdateInstallerPricingAsync(Guid installerId, InstallerPricing InstallerPricing);
        /*Task<InstallerPricing> CreateInstallerPricingAsync(InstallerPricing InstallerPricing);*/
        Task DeleteInstallerPricingAsync(Guid installerId);

        Task<InstallerPricing> GetInstallerPicing(Guid installerId, int productId);
    }
}
