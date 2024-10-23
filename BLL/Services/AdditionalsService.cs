using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdditionalsService : IAdditionalsService
    {
        private readonly IAdditionalsRepository _additionalsRepository;
        private readonly IMapper _mapper;

        public AdditionalsService(IAdditionalsRepository AdditionalsRepository, IMapper mapper)
        {
            _additionalsRepository = AdditionalsRepository;
            _mapper = mapper;
        }

        public async Task<AdditionalVm> CreateAsync(AdditionalVm additional)
        {
            var entity = _mapper.Map<Additional>(additional);
            var newEn = await _additionalsRepository.CreateAsync(entity);
            var vm = _mapper.Map<AdditionalVm>(newEn);
            return vm;

        }

        public async Task DeleteAsync(Guid id)
        {
            var result = await _additionalsRepository.DeleteAsync(id);
        }

        public async Task<List<AdditionalVm>> GetAsync()
        {
            var entities = await _additionalsRepository.GetAsync();
            var vms = _mapper.Map<List<AdditionalVm>>(entities);
            return vms;
        }

        public async Task<AdditionalVm> UpdateAsync(AdditionalVm additional)
        {
            var ent = _mapper.Map<Additional>(additional); 
            var newEnt = await _additionalsRepository.UpdateAsync(ent);
            return _mapper.Map< AdditionalVm>(newEnt);
        }
    }
}
