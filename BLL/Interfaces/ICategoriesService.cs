using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoriesService
    {
        Task<List<CategoryVm>> GetCategoriesAsync();
        Task<CategoryVm> GetCategoryAsync(Guid id);
        Task<CategoryVm> CreateCategoryAsync(Category category);
        Task<CategoryVm> UpdateCategoryAsync(Guid id, Category category);
        Task DeleteCategoryAsync(Guid id);
    }
}
