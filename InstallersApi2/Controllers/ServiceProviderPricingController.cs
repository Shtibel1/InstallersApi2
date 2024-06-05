using BLL.DTOs;
using BLL.Interfaces;
using BLL.Vms;
using DAL.Entities;
using DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstallersApi2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderPricingController : ControllerBase
    {
        private readonly IServiceProviderPricingService _InstallerPricingService;

        public ServiceProviderPricingController(IServiceProviderPricingService InstallerPricingService)
        {
            _InstallerPricingService = InstallerPricingService;
        }

        [HttpGet("{installerId}")]
        [Authorize]
        public async Task<ActionResult<ServiceProviderPricing>> GetPricesByInstaller(Guid installerId)
        {
            try
            {

                return Ok(await _InstallerPricingService.GetPricingByServiceProviderAsync(installerId));
            }
            catch (Exception)
            {
                return BadRequest("FAILED_GET_InstallerPricing");
            }

        }

        [HttpPut("{installerId}")]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<ActionResult<List<DAL.Entities.ServiceProviderPricing>>> PutPrice(Guid installerId, List<ServiceProviderPricingVm> InstallerPricingChart)
        {
            try
            {
                var instllerPricing = await _InstallerPricingService.UpdateServiceProviderPricingAsync(installerId, InstallerPricingChart);
                return Ok(instllerPricing);
            }
            catch (Exception ex)
            {
                return BadRequest("FAILED_UPDATE_InstallerPricing");
            }
        }

        [HttpGet("{installerId}/{productId}")]
        [Authorize]
        public async Task<ActionResult<ServiceProviderPricingVm>> GetPricesByInstallerProduct(string installerId, Guid productId)
        {
            try
            {
                var installerIdGuid = new Guid(installerId);
                var prices = await _InstallerPricingService.GetServiceProviderPricingByProductVmAsync(installerIdGuid, productId);
                return Ok(prices);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("prices-comparison")]
        [Authorize]
        public async Task<ActionResult<List<ServiceProviderPricingVm>>> GetPricesComparison(PricesComparisonRequest pricesComparisonRequest)
        {
            return Ok(await _InstallerPricingService.PricesComprasion(pricesComparisonRequest));
        }

        /*[HttpPost]
        public async Task<ActionResult<List<InstallerPricing>>> PostPrice(List<InstallerPricing> InstallerPricing)
        {
            try
            {
                return Ok(await _InstallerPricingService.CreateInstallerPricingAsync(InstallerPricing));
            }
            catch (Exception)
            {
                return BadRequest("FAILED_CREATE_InstallerPricing");
            }
        }*/

    }

    
}
