using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Abstracts;
using DAL.Entities;
using DAL.Enums;
using DAL.Interfaces;
using DAL.Providers;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceProvidersService : ISPService
    {
        private readonly IServiceProvidersRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ICompanyDataProvider _companyDataProvider;
        private readonly ICategoriesRepository _categoriesRepository;

        public ServiceProvidersService(IServiceProvidersRepository accountRepository, IMapper mapper, ICompanyDataProvider companyDataProvider, ICategoriesRepository categoriesRepository)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _companyDataProvider = companyDataProvider;
            _categoriesRepository = categoriesRepository;
        }

        public async Task<ServiceProviderVm> CreateServiceProviderAsync(CreateServiceProviderVm ServiceProvider)
        {
            var entity = _mapper.Map<ServiceProvider>(ServiceProvider);
            var company = _companyDataProvider.GetCompanies()[0];
            var createdServiceProvider = await _accountRepository.CreateServiceProviderAsync(entity, company);   

            var categories = await _categoriesRepository.AddCategoriesToServiceProviderAsync(createdServiceProvider.Id, ServiceProvider.Categories);

            var insVms = _mapper.Map<ServiceProviderVm>(createdServiceProvider); 
            var catVms =  _mapper.Map<List<CategoryVm>>(categories);

            insVms.Categories = catVms;

            return insVms;
        }

        public async Task<List<ServiceProviderVm>> GetServiceProvidersAsync(List<CompanyNames> companyNames)
        {

            var vms = new List<ServiceProviderVm>();
            var serviceProviders = await _accountRepository.GetServiceProvidersByCompaniesAsync(companyNames);
            var serviceProviderIds = serviceProviders.Select(sp => sp.Id).ToList();
            var serviceProviderCategories = await _categoriesRepository.GetCategoriesByServiceProvidersAsync(serviceProviderIds);

            foreach (var serviceProvider in serviceProviders)
            {
                var serviceProviderVm = new ServiceProviderVm 
                {
                    Id = serviceProvider.Id,
                    Name = serviceProvider.Name,
                    Phone = serviceProvider.Phone,
                    Role = Enum.Parse<Role>(serviceProvider.Role) 
                };
                if (serviceProviderCategories.ContainsKey(serviceProvider.Id))
                {
                    
                    serviceProviderVm.Categories = _mapper.Map<List<CategoryVm>>(serviceProviderCategories[serviceProvider.Id]);
                }
                    vms.Add(serviceProviderVm);

            }

            return vms;

        }

        public async Task<ServiceProviderVm> GetServiceProviderAsync(Guid id)
        {
            var entity = await _accountRepository.GetserviceProviderAsync(id);
            var serviceProviderCategories = await _categoriesRepository.GetCategoriesByServiceProvidersAsync(new List<Guid> { id });
            var vm =  _mapper.Map<ServiceProviderVm>(entity);
            vm.Categories = _mapper.Map<List<CategoryVm>>(serviceProviderCategories[id]);
            return vm;
        }
    }
}
