using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using BLL.Interfaces;
using DAL.Enums;

namespace InstallersApi2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InstallerPricingController : ControllerBase
    {
        private readonly IInstallerPricingService _InstallerPricingService;

        public InstallerPricingController(IInstallerPricingService InstallerPricingService)
        {
            _InstallerPricingService = InstallerPricingService;
        }

        [HttpGet("{installerId}")]
        [Authorize]
        public async Task<ActionResult<DAL.Entities.InstallerPricing>> GetPricesByInstaller(Guid installerId)
        {
            try
            {

                return Ok(await _InstallerPricingService.GetPricingByInstallerAsync(installerId));
            }
            catch (Exception)
            {
                return BadRequest("FAILED_GET_InstallerPricing");
            }

        }

        [HttpPut("{installerId}")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<ActionResult<List<DAL.Entities.InstallerPricing>>> PutPrice(Guid installerId, List<InstallerPricingVm> InstallerPricingChart)
        {
            try
            {
                var instllerPricing = await _InstallerPricingService.UpdateInstallerPricingAsync(installerId, InstallerPricingChart);
                return Ok(instllerPricing);
            }
            catch (Exception ex)
            {
                return BadRequest("FAILED_UPDATE_InstallerPricing");
            }
        }

        [HttpGet("{installerId}/{productId}")]
        [Authorize]
        public async Task<ActionResult<InstallerPricingVm>> GetPricesByInstallerProduct(string installerId, int productId)
        {
            try
            {
                var installerIdGuid = new Guid(installerId);
                var prices = await _InstallerPricingService.GetInstallerPricingByProductVmAsync(installerIdGuid, productId);
                return Ok(prices);
            }
            catch (Exception)
            {

                throw;
            }
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
