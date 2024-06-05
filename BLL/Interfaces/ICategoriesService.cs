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
        Task<CategoryVm> CreateCategoryAsync(CategoryVm categoryVm);
        Task DeleteCategoryAsync(Guid id);
        Task<CategoryVm> GetCategoryAsync(Guid id);
        Task<List<CategoryVm>> GetCategoriesAsync();
        Task<CategoryVm> UpdateCategoryAsync(Guid id, CategoryVm categoryVm);
    }
}
