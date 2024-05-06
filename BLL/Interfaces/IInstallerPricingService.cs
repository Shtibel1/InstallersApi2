using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IInstallerPricingService
    {
        Task<List<InstallerPricingVm>> GetPricingByInstallerAsync(Guid installerId);
        Task<List<InstallerPricingVm>> UpdateInstallerPricingAsync(Guid installerId, List<InstallerPricingVm> InstallerPricing);
        Task<InstallerPricingVm> GetInstallerPricingByProductVmAsync(Guid installerId, int productId);

    }
}
