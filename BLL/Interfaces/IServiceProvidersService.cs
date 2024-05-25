using BLL.Models;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IServiceProvidersService
    {
        Task<List<ServiceProviderVm>> GetServiceProvidersAsync(List<CompanyNames> companyNames);
        Task<List<ServiceProviderVm>> GetServiceProviderAsync(Guid id);
        Task<ServiceProviderVm> CreateServiceProviderAsync(CreateServiceProviderVm installer);
    }
}
