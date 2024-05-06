using BLL.Models;
using BLL.Services.AuthService;
using Business;
using DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;


namespace Ins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]


        [HttpPost("signup")]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> SignUp([FromBody] SignupModel signUp)
        {
            var response = await _authService.SignupAsync(signUp);
            if (response.UserIsAlreadyExist) return BadRequest("user is already exist");
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LoginModel login)
        {
            var user = await _authService.LoginAsync(login);
            if (user != null) return Ok(user);
            return Unauthorized();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(string id)
        {

            var user = await _authService.DeleteAsync(id);
            if (user) return NoContent();

            return BadRequest(new { message = "FAILED_DELETE" });
        }
    }
}
