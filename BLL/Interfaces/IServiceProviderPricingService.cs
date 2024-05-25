using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IServiceProviderPricingService
    {
        Task<List<ServiceProviderPricingVm>> GetPricingByServiceProviderAsync(Guid installerId);
        Task<List<ServiceProviderPricingVm>> UpdateServiceProviderPricingAsync(Guid installerId, List<ServiceProviderPricingVm> InstallerPricing);
        Task<ServiceProviderPricingVm> GetServiceProviderPricingByProductVmAsync(Guid installerId, Guid productId);

    }
}
