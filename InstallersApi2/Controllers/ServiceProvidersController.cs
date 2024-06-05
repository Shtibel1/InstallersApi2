using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Enums;
using ServiceProvidersApi2.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.Providers;

namespace ServiceProvidersApi2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProvidersController : ControllerBase
    {
        private readonly ISPService _ServiceProvidersService;
        private readonly ICompanyDataProvider _companyDataProvider;

        public ServiceProvidersController(ISPService ServiceProvidersService, ICompanyDataProvider companyDataProvider)
        {
            _ServiceProvidersService = ServiceProvidersService;
            _companyDataProvider = companyDataProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceProviders()
        {
            try
            {
                var companyName = _companyDataProvider.GetCompanies();

                var ServiceProviders = await _ServiceProvidersService.GetServiceProvidersAsync(companyName);
                return Ok(ServiceProviders);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceProvider(Guid id)
        {
            try
            {
                var ServiceProvider = await _ServiceProvidersService.GetServiceProviderAsync(id);
                return Ok(ServiceProvider);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Employee))]
        public async Task<IActionResult> CreateServiceProvider([FromBody] CreateServiceProviderVm ServiceProvider)
        {
            try
            {
                var newServiceProvider = await _ServiceProvidersService.CreateServiceProviderAsync(ServiceProvider);
                return Ok(newServiceProvider);
            }
            catch (Exception ex)
            {
                return BadRequest("FAILED_CREATED");
            }
        }
    }
}
