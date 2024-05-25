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
    public interface IServiceProviderRepository
    {
        Task<List<ServiceProvider>> GetServiceProvidersByCompaniesAsync(List<CompanyNames> companyNames);
        Task<List<ServiceProvider>> GetserviceProviderAsync(Guid id);
        Task<ServiceProvider> CreateserviceProviderAsync(ServiceProvider installer);
        Task<ServiceProvider?> GetserviceProviderbyUserId(Guid id);
    }
}
