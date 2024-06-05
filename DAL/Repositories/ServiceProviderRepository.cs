using DAL.Abstracts;
using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ServiceProviderRepository : IServiceProvidersRepository
    {
        private readonly CentralDbContext _context;

        public ServiceProviderRepository(CentralDbContext context)
        {

            _context = context;
        }

        public async Task<List<ServiceProvider>> GetServiceProvidersByCompaniesAsync(List<CompanyNames> companyNames)
        {
            var companyNamesSet = companyNames.Select(cn => cn.ToString()).ToHashSet();

            var serviceProviders = await _context.ServiceProviders
                .Where(sp => sp.Companies.Any(ac => companyNamesSet.Contains(ac.Name)))
                .ToListAsync();

            return serviceProviders;
        }

        public async Task<ServiceProvider> CreateServiceProviderAsync(ServiceProvider serviceProvider, CompanyNames companyName)
        {
            serviceProvider.Id = Guid.NewGuid();
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var company = await _context.Companies
                    .FirstOrDefaultAsync(c => c.Name == companyName.ToString());

                if (company != null)
                {
                    serviceProvider.Companies.Add(company);
                }

                await _context.ServiceProviders.AddAsync(serviceProvider);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return serviceProvider;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<ServiceProvider> GetserviceProviderAsync(Guid id)
        {
            var serviceProvider = await _context.ServiceProviders
                .FirstOrDefaultAsync(i => i.Id == id);

            return serviceProvider;

        }

        public async Task<ServiceProvider?> GetserviceProviderbyUserId(Guid id)
        {
            return await _context.ServiceProviders
                .Include(sp => sp.Companies)
                .FirstOrDefaultAsync(manager => manager.IdentityId == id);
        }

        public async Task<List<ServiceProvider>> GetServiceProvidersByIds(List<Guid> Ids)
        {
            return await _context.ServiceProviders
                .Where(sp => Ids.Contains(sp.Id))
                .ToListAsync();
        }
    }


}
