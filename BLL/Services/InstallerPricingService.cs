using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class InstallerPricingService : IInstallerPricingService
    {
        private readonly IInstallerPricingRepository _InstallerPricingRepository;
        private readonly IMapper _mapper;

        public InstallerPricingService(IInstallerPricingRepository InstallerPricingRepository, IMapper mapper)
        {
            _InstallerPricingRepository = InstallerPricingRepository;
            _mapper = mapper;
        }
        public async Task<List<InstallerPricingVm>> GetPricingByInstallerAsync(Guid installerId)
        {
            var InstallerPricingChart = _mapper.Map<List<InstallerPricingVm>>(await _InstallerPricingRepository.GetInstallerPricingByInstallerAsync(installerId));
            return InstallerPricingChart;
        }

        public async Task<List<InstallerPricingVm>> UpdateInstallerPricingAsync(Guid installerId, List<InstallerPricingVm> InstallerPricingChart)
        {
            await _InstallerPricingRepository.DeleteInstallerPricingAsync(installerId);
            List<InstallerPricingVm> updatedInstallerPricingChart = new List<InstallerPricingVm>();
            for (int i = 0; i < InstallerPricingChart.Count; i++)
            {
                var entity = _mapper.Map<InstallerPricing>(InstallerPricingChart[i]);
                var updatedPricing = await _InstallerPricingRepository.UpdateInstallerPricingAsync(installerId, entity);
                var InstallerPricing = _mapper.Map<InstallerPricingVm>(updatedPricing);
                updatedInstallerPricingChart.Add(InstallerPricing);
            }
            return updatedInstallerPricingChart;
        }

        public async Task<InstallerPricingVm> GetInstallerPricingByProductVmAsync(Guid installerId, int productId)
        {
            var prices = await _InstallerPricingRepository.GetInstallerPicing(installerId, productId);
            var pricesVm = _mapper.Map<InstallerPricingVm>(prices);
            return pricesVm;
        }



        /*public async Task<List<InstallerPricing>> CreateInstallerPricingAsync(List<InstallerPricing> InstallerPricing)
        {
            List<InstallerPricing> createdInstallerPricing = new List<InstallerPricing>(); 
            for (int i = 0; i < InstallerPricing.Count; i++)
            {
                createdInstallerPricing.Add(await _InstallerPricingRepository.CreateInstallerPricingAsync(InstallerPricing[i]));
            }
            return createdInstallerPricing;
        }*/
        /*public async Task DeleteInstallerPricingAsync(int id)
        {
            
        }*/


    }
}
