using DAL.Entities;
using DAL.Providers;

namespace InstallersApi2.Controllers
{
    public class ServiceProviderCategoryRepository : IServiceProviderCategoryRepository
    {
        private readonly ICompanyDataProvider _companyDataProvider;

        public ServiceProviderCategoryRepository(ICompanyDataProvider companyDataProvider)
        {
            _companyDataProvider = companyDataProvider;
        }

        public async Task<bool> AddCategoriesToServiceProviderAsync(List<Category> categories, Guid serviceProviderId)
        {
            var context = _companyDataProvider.GetContexts()[0];    
            var serviceProviderCategories = new List<ServiceProviderCategory>();

            //delete all categories for this serviceProvider
            var result = context.ServiceProviderCategories.Where(x => x.ServiceProviderIdExternal == serviceProviderId);
            context.ServiceProviderCategories.RemoveRange(result);

            foreach (var category in categories)
            {
                serviceProviderCategories.Add(new ServiceProviderCategory
                {
                    CategoryId = category.Id,
                    ServiceProviderIdExternal = serviceProviderId
                });
            }
            await context.ServiceProviderCategories.AddRangeAsync(serviceProviderCategories);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
