using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Enums;
using InstallersApi2.Controllers;
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

namespace InstallersApi2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InstallersController : ControllerBase
    {
        private readonly IInstallersService _installersService;

        public InstallersController(IInstallersService installersService)
        {
            _installersService = installersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInstallers()
        {
            try
            {
                var installers = await _installersService.GetInstallersAsync();
                return Ok(installers);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstaller(Guid id)
        {
            try
            {
                var installer = await _installersService.GetInstallerAsync(id);
                return Ok(installer);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> CreateInstaller([FromBody] CreateInstallerModel installer)
        {
            try
            {
                var newInstaller = await _installersService.CreateInstallerAsync(installer);
                return Ok(newInstaller);
            }
            catch (Exception ex)
            {
                return BadRequest("FAILED_CREATED");
            }
        }
    }
}
