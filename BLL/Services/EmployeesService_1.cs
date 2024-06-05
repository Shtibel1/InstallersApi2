using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using InstallersApi2.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _managersRepository;
        private readonly IMapper _mapper;
        private readonly IServiceProviderCategoryRepository _serviceProviderCategoryRepository;
        private readonly ISPService _serviceProviderIsService;

        public EmployeesService(IEmployeesRepository managersRepository, IMapper mapper, IServiceProviderCategoryRepository serviceProviderCategoryRepository, ISPService serviceProviderIsService)
        {
            _managersRepository = managersRepository;
            _mapper = mapper;
            _serviceProviderCategoryRepository = serviceProviderCategoryRepository;
            _serviceProviderIsService = serviceProviderIsService;
        }

        public async Task<EmployeeVm> CreateManagerAsync(EmployeeVm manager)
        {
            var entity = _mapper.Map<Employee>(manager);

            var newManEntity = await _managersRepository.CreateEmployeeAsync(entity);
            var newMan = _mapper.Map<EmployeeVm>(newManEntity);
            return newMan;
        }

        public Task<List<EmployeeVm>> GetManagersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceProviderVm> AddCategoriesToServiceProviderAsync(List<CategoryVm> categories, Guid serviceProviderId)
            {
            var entities = _mapper.Map<List<Category>>(categories);
            var succeed = await _serviceProviderCategoryRepository.AddCategoriesToServiceProviderAsync(entities, serviceProviderId);

            if (succeed == true)
            {
                var updatedEntity = await _serviceProviderIsService.GetServiceProviderAsync(serviceProviderId);
                var vm = _mapper.Map<ServiceProviderVm>(updatedEntity);
                return vm;
            }

            return null;

        }



    }
}
