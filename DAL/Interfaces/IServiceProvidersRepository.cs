using DAL.Abstracts;
using DAL.Entities;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IServiceProvidersRepository
    {
        Task<List<ServiceProvider>> GetServiceProvidersByCompaniesAsync(List<CompanyNames> companyNames);
        Task<ServiceProvider> GetserviceProviderAsync(Guid id);
        Task<ServiceProvider> CreateServiceProviderAsync(ServiceProvider serviceProvider, CompanyNames companyName);
        Task<ServiceProvider?> GetserviceProviderbyUserId(Guid id);
        Task<List<ServiceProvider>> GetServiceProvidersByIds(List<Guid> Ids);
    }
}
