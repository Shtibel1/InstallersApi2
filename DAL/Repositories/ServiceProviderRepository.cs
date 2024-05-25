using DAL.Abstracts;
using DAL.Data;
using DAL.Enums;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ServiceProviderRepository : IServiceProviderRepository
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

        public async Task<ServiceProvider> CreateserviceProviderAsync(ServiceProvider serviceProvider)
        {
            serviceProvider.Id = Guid.NewGuid();

            await _context.ServiceProviders.AddAsync(serviceProvider);
            await _context.SaveChangesAsync();
            return serviceProvider;

        }

        public async Task<List<ServiceProvider>> GetserviceProviderAsync(Guid id)
        {
            var serviceProvider = await _context.ServiceProviders
                .FirstOrDefaultAsync(i => i.Id == id);

            return serviceProvider != null ? new List<ServiceProvider> { serviceProvider } : new List<ServiceProvider>();

        }

        public async Task<ServiceProvider?> GetserviceProviderbyUserId(Guid id)
        {
            return await _context.ServiceProviders
                .Include(sp => sp.Companies)
                .FirstOrDefaultAsync(manager => manager.IdentityId == id);
        }
    }


}
