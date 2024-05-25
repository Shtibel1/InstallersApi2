using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ICategoriesRepository
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(Guid id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Guid id, Category category);
        Task DeleteCategoryAsync(Guid id);

        Task AddCategoriesToServiceProviderAsync(Guid serviceProviderId, List<Guid> categoryIds);
    }
}
