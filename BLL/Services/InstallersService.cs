using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Abstracts;
using DAL.Entities;
using DAL.Enums;
using DAL.Interfaces;
using DAL.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceProvidersService : IServiceProvidersService
    {
        private readonly IServiceProviderRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ICompanyDataProvider _companyDataProvider;

        public ServiceProvidersService(IServiceProviderRepository accountRepository, IMapper mapper, ICompanyDataProvider companyDataProvider)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _companyDataProvider = companyDataProvider;
        }

        public async Task<ServiceProviderVm> CreateServiceProviderAsync(CreateServiceProviderVm ServiceProvider)
        {
            var entity = _mapper.Map<ServiceProvider>(ServiceProvider);
            var newIns = _mapper.Map<ServiceProviderVm>(await _accountRepository.CreateserviceProviderAsync(entity));
            return newIns;
        }

        public async Task<List<ServiceProviderVm>> GetServiceProvidersAsync(List<CompanyNames> companyNames)
        {
             

            var entities = await _accountRepository.GetServiceProvidersByCompaniesAsync(companyNames);
            
            var vms = _mapper.Map<List<ServiceProviderVm>>(entities);

            return vms;
        }

        public async Task<List<ServiceProviderVm>> GetServiceProviderAsync(Guid id)
        {
            var entity = await _accountRepository.GetserviceProviderAsync(id);
            return _mapper.Map<List<ServiceProviderVm>>(entity);
        }
    }
}
