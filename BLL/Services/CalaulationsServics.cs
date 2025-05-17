using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Vms;
using DAL.Abstracts;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CalaulationsServics : ICalaulationsService
    {
        private readonly ICalaulationsRepository _calaulationsRepository;
        private readonly IMapper _mapper;
        private readonly IServiceProvidersRepository _serviceProvidersRepository;

        public CalaulationsServics(ICalaulationsRepository calaulationsRepository, IMapper mapper, IServiceProvidersRepository serviceProvidersRepository)
        {
            _calaulationsRepository = calaulationsRepository;
            _mapper = mapper;
            _serviceProvidersRepository = serviceProvidersRepository;
        }
        public async Task Create(CalaulationVm calaulationVm)
        {
            var entity = _mapper.Map<Calculation>(calaulationVm);
            entity.CalculationAssignments = calaulationVm.AssignmentIds.Select(c => new CalculationAssignment
            {
                AssignmentId = c
            }).ToList();
            await _calaulationsRepository.Create(entity);

        }

        public async Task<CalaulationVm> Get(Guid id)
        {
            var entity = await _calaulationsRepository.Get(id);
            return _mapper.Map<CalaulationVm>(entity);
        }

        public async Task<List<CalaulationVm>> Get()
        {
            var entities = await _calaulationsRepository.Get();
            var vms  = _mapper.Map<List<CalaulationVm>>(entities);

            foreach (var vm in vms)
            {

                vm.ServiceProvider = _mapper.Map<ServiceProviderVm> (await _serviceProvidersRepository.GetserviceProviderAsync(vm.ServiceProviderId));
            }

            return vms;

        }
    }
}
