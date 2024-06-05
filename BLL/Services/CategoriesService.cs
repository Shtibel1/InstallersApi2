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

        public async Task<CategoryVm> CreateCategoryAsync(CategoryVm categoryVm)
        {
            var entity = await _categoriesRepository.CreateCategoryAsync(_mapper.Map<Category>(categoryVm));
            var vm = _mapper.Map<CategoryVm>(entity);
            return vm;
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoriesRepository.DeleteCategoryAsync(id);
        }

        public async Task<CategoryVm> GetCategoryAsync(Guid id)
        {
            var vm = _mapper.Map<CategoryVm>(await _categoriesRepository.GetCategoryAsync(id));
            return vm;
        }

        public async Task<List<CategoryVm>> GetCategoriesAsync()
        {
            var vms = _mapper.Map<List<CategoryVm>>(await _categoriesRepository.GetCategoriesAsync());
            return vms;
        }

        public async Task<CategoryVm> UpdateCategoryAsync(Guid id, CategoryVm categoryVm)
        {
            var entity = await _categoriesRepository.UpdateCategoryAsync(id, _mapper.Map<Category>(categoryVm));
            return _mapper.Map<CategoryVm>(entity);
        }
    }
}
