using AutoMapper;
using BLL.Interfaces;
using BLL.Vms;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdditionalPriceService : IAdditionalPriceService
    {
        private readonly IAdditionalPriceRepository _additionalPriceRepository;
        private readonly IMapper _mapper;

        public AdditionalPriceService(IAdditionalPriceRepository additionalPriceRepository, IMapper mapper)
        {
            _additionalPriceRepository = additionalPriceRepository;
            _mapper = mapper;
        }

        public async Task<List<AdditionalPriceVm>> CreateAsync(List<AdditionalPriceVm> additionals)
        {
            var additionalPrices = _mapper.Map<List<AdditionalPrice>>(additionals);
            var createdAdditionalPrices = await _additionalPriceRepository.CreateAsync(additionalPrices);
            return _mapper.Map<List<AdditionalPriceVm>>(createdAdditionalPrices);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _additionalPriceRepository.DeleteAsync(id);
        }

        public async Task<List<AdditionalPriceVm>> Get(Guid SPId, Guid productId)
        {
            var additionalPrice = await _additionalPriceRepository.Get(SPId, productId);
            return _mapper.Map<List<AdditionalPriceVm>>(additionalPrice);
        }

        public async Task<List<AdditionalPriceVm>> GetBySP(Guid SPId)
        {
            var additionalPrices = await _additionalPriceRepository.GetBySP(SPId);
            return _mapper.Map<List<AdditionalPriceVm>>(additionalPrices);
        }

        public async Task<List<AdditionalPriceVm>> UpdateAsync(List<AdditionalPriceVm> additionals)
        {
            var additionalPrices = _mapper.Map<List<AdditionalPrice>>(additionals);
            var updatedAdditionalPrices = await _additionalPriceRepository.UpdateAsync(additionalPrices);
            return _mapper.Map<List<AdditionalPriceVm>>(updatedAdditionalPrices);
        }
    }

}
