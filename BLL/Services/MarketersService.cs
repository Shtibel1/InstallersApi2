using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MarketersService : IMarketersService
    {
        private readonly IMarketersRepository _marketersRepository;
        private readonly IMapper _mapper;

        public MarketersService(IMarketersRepository marketersRepository, IMapper mapper)
        {
            _marketersRepository = marketersRepository;
            _mapper = mapper;
        }

        public async Task<MarketerVm> CreateMarketerAsync(MarketerVm marketer)
        {

            var entity = await _marketersRepository.CreateMarketerAsync(_mapper.Map<Marketer>(marketer));
            var vm = _mapper.Map<MarketerVm>(entity);
            return vm;
        }

        public async Task DeleteMarketerAsync(Guid id)
        {
            await _marketersRepository.DeleteMarketerAsync(id);
        }

        public async Task<MarketerVm> GetMarketerAsync(Guid id)
        {
            var vm = _mapper.Map<MarketerVm>(await _marketersRepository.GetMarketerAsync(id));
            return vm;
        }

        public async Task<List<MarketerVm>> GetMarketersAsync()
        {
            var vms = _mapper.Map<List<MarketerVm>>(await _marketersRepository.GetMarketersAsync());
            return vms;
        }

        public async Task<MarketerVm> UpdateMarketerAsync(MarketerVm marketer)
        {
           var entity = _mapper.Map<MarketerVm>(marketer);
            return entity;
        }
    }
}
