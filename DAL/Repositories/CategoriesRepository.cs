using DAL.Abstracts;
using DAL.Data;
using DAL.Entities;
using DAL.Providers;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ICompanyDataProvider _companyDataProvider;

        public CategoriesRepository(ICompanyDataProvider companyDataProvider)
        {
            _companyDataProvider = companyDataProvider;

        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var context = _companyDataProvider.GetContexts()[0];
            return await context.Categories.ToListAsync();
        }

        public async Task<Dictionary<Guid, List<Category>>> GetCategoriesByServiceProvidersAsync(List<Guid> serviceProviderIds)
        {
            var context = _companyDataProvider.GetContexts()[0];

            return await context.ServiceProviderCategories
                .Where(spc => serviceProviderIds.Contains(spc.ServiceProviderIdExternal))
                .Include(spc => spc.Category)
                .GroupBy(spc => spc.ServiceProviderIdExternal)
                .ToDictionaryAsync(g => g.Key, g => g.Select(spc => spc.Category).ToList());
        }

        public async Task<Category> GetCategoryAsync(Guid id)
        {
            var context = _companyDataProvider.GetContexts()[0];
            return  await context.Categories.FindAsync(id);
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var context = _companyDataProvider.GetContexts()[0];
            var result = await context.Categories.AddAsync(category).ConfigureAwait(false);
            if (result != null && result.Entity != null)
            {
                var newCat = result.Entity;
                await context.SaveChangesAsync();
                return newCat;
            }
            await context.SaveChangesAsync();
            return null;
        }
        public async Task<Category> UpdateCategoryAsync(Guid id, Category category)
        {
            var context = _companyDataProvider.GetContexts()[0];
            var result = context.Categories.Update(category);
            if (result != null && result.Entity != null)
            {
                var updatedCat = result.Entity;
                await context.SaveChangesAsync();
                return updatedCat;
            }
            await context.SaveChangesAsync();
            return null;
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var context = _companyDataProvider.GetContexts()[0];
            var category = await context.Categories.FindAsync(id);
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }

        public async Task<List<Category>> AddCategoriesToServiceProviderAsync(Guid serviceProviderId, List<Guid> categoryIds)
        {
            var context = _companyDataProvider.GetContexts()[0];

            var serviceProviderCategories = categoryIds.Select(categoryId => new ServiceProviderCategory
            {
                ServiceProviderIdExternal = serviceProviderId,
                CategoryId = categoryId
            });

            await context.ServiceProviderCategories.AddRangeAsync(serviceProviderCategories);
            await context.SaveChangesAsync();
            
            return await context.Categories.Where(c => categoryIds.Contains(c.Id)).ToListAsync();
        }


    }
}
