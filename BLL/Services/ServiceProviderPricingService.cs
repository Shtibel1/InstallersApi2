using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Vms;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class ServiceProviderPricingService : IServiceProviderPricingService
    {
        private readonly IServiceProviderPricingRepository _InstallerPricingRepository;
        private readonly IMapper _mapper;

        public ServiceProviderPricingService(IServiceProviderPricingRepository InstallerPricingRepository, IMapper mapper)
        {
            _InstallerPricingRepository = InstallerPricingRepository;
            _mapper = mapper;
        }
        public async Task<List<ServiceProviderPricingVm>> GetPricingByServiceProviderAsync(Guid installerId)
        {
            var InstallerPricingChart = _mapper.Map<List<ServiceProviderPricingVm>>(await _InstallerPricingRepository.GetInstallerPricingByInstallerAsync(installerId));
            return InstallerPricingChart;
        }

        public async Task<List<ServiceProviderPricingVm>> UpdateServiceProviderPricingAsync(Guid installerId, List<ServiceProviderPricingVm> InstallerPricingChart)
        {
            await _InstallerPricingRepository.DeleteInstallerPricingAsync(installerId);
            List<ServiceProviderPricingVm> updatedInstallerPricingChart = new List<ServiceProviderPricingVm>();
            for (int i = 0; i < InstallerPricingChart.Count; i++)
            {
                var entity = _mapper.Map<ServiceProviderPricing>(InstallerPricingChart[i]);
                var updatedPricing = await _InstallerPricingRepository.UpdateInstallerPricingAsync(installerId, entity);
                var InstallerPricing = _mapper.Map<ServiceProviderPricingVm>(updatedPricing);
                updatedInstallerPricingChart.Add(InstallerPricing);
            }
            return updatedInstallerPricingChart;
        }

        public async Task<ServiceProviderPricingVm> GetServiceProviderPricingByProductVmAsync(Guid installerId, Guid productId)
        {
            var prices = await _InstallerPricingRepository.GetServiceProviderPicing(installerId, productId);
            var pricesVm = _mapper.Map<ServiceProviderPricingVm>(prices);
            return pricesVm;
        }

        public async Task<List<ServiceProviderPricingVm>> PricesComprasion(PricesComparisonRequest pricesComparisonRequest)
        {
            var prices = await _InstallerPricingRepository.PricesComprasion(pricesComparisonRequest.ServiceProviderIds, pricesComparisonRequest.ProductId);
            var pricesVm = _mapper.Map<List<ServiceProviderPricingVm>>(prices);
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
