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
        Task<List<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoryAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(Category category);
        Task<CategoryDto> UpdateCategoryAsync(int id, Category category);
        Task DeleteCategoryAsync(int id);
    }
}
