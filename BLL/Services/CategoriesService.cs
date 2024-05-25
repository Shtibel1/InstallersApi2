using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public CategoriesService(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }

        public async Task<CategoryVm> CreateCategoryAsync(Category category)
        {
            var newCat = await _categoriesRepository.CreateCategoryAsync(category);
            return _mapper.Map<CategoryVm>(newCat);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
             await _categoriesRepository.DeleteCategoryAsync(id);
        }

        public async Task<List<CategoryVm>> GetCategoriesAsync()
        {
            var categories = await _categoriesRepository.GetCategoriesAsync();
            return _mapper.Map<List<CategoryVm>>(categories);
        }

        public async Task<CategoryVm> GetCategoryAsync(Guid id)
        {

            var category = await _categoriesRepository.GetCategoryAsync(id);
            return _mapper.Map<CategoryVm>(category);
        }

        public async Task<CategoryVm> UpdateCategoryAsync(Guid id, Category category)
        {
            var updatedCat = await _categoriesRepository.UpdateCategoryAsync(id, category);
            return _mapper.Map<CategoryVm>(updatedCat);
        }
    }
}
