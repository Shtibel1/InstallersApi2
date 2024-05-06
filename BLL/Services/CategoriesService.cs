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

        public async Task<CategoryDto> CreateCategoryAsync(Category category)
        {
            var newCat = await _categoriesRepository.CreateCategoryAsync(category);
            return _mapper.Map<CategoryDto>(newCat);
        }

        public async Task DeleteCategoryAsync(int id)
        {
             await _categoriesRepository.DeleteCategoryAsync(id);
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoriesRepository.GetCategoriesAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryAsync(int id)
        {

            var category = await _categoriesRepository.GetCategoryAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int id, Category category)
        {
            var updatedCat = await _categoriesRepository.UpdateCategoryAsync(id, category);
            return _mapper.Map<CategoryDto>(updatedCat);
        }
    }
}
