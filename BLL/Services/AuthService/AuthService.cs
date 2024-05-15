using AutoMapper;
using BLL.DTOs.Abstracts;
using BLL.Exceptions;
using BLL.Models;
using Business;
using Business.Models;
using DAL.Entities;
using DAL.Enums;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _loginManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IManagersRepository _managersRepository;
        private readonly IInstallersRepository _installersRepository;
        private HttpClient client = new HttpClient();
        public AuthService(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> loginManager,
           IConfiguration configuration,
           IMapper mapper,
           IManagersRepository managersRepository,
           IInstallersRepository installersRepository)
        {
            _userManager = userManager;
            _loginManager = loginManager;
            _configuration = configuration;
            _mapper = mapper;
            _managersRepository = managersRepository;
            _installersRepository = installersRepository;
        }

        public async Task<AppUserModel?> LoginAsync(LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null) return null;

            var signInResult = await _loginManager.PasswordSignInAsync(user.UserName, login.Password, false, false);
            if (!signInResult.Succeeded) return null;

            var roles = await _userManager.GetRolesAsync(user);

            Guid workerId;
            if (roles[0] == Roles.Manager)
            {
               var worker = await this._managersRepository.GetManagerbyUserId(user.Id);
                workerId = worker.Id;
            }
            else
            {
                var worker = await _installersRepository.GetInstallerbyUserId(user.Id);
                workerId = worker.Id;
            }
            var token = generateToken(user, roles[0]);

            return new AppUserModel { Id = workerId, Name = user.UserName, Token = token, Role = roles[0] };
        }

        public async Task<SignupServiceResponse?> SignupAsync(SignupModel signUp)
        {
            var user = await _userManager.FindByNameAsync(signUp.Name);
            if (user is not null) return new SignupServiceResponse { UserIsAlreadyExist = true };
            var savedUser = await SaveUser(signUp, signUp.Role);
            var savedWorker = await SaveWorkerAsync(signUp, savedUser.Id);
            return new SignupServiceResponse { UserIsAlreadyExist = false };
        }

        private async Task<Worker> SaveWorkerAsync(SignupModel signUp, string identityId)
        {
            var worker = new Worker();
            switch (signUp.Role)
            {
                case "manager":
                    var managerEntity = _mapper.Map<Manager>(signUp);
                    managerEntity.IdentityId = identityId;
                    worker = await _managersRepository.CreateManagerAsync(managerEntity);
                    break;
                    
                case "installer":

                    var installerEntity = _mapper.Map<Installer>(signUp);
                    installerEntity.IdentityId = identityId;
                    worker = await _installersRepository.CreateInstallerAsync(installerEntity, signUp.Categories);
                    break;
                default: throw new Exception("cannot find role in signupModel");
            }

            return worker;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            if (user == null)
            {
                return false;
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }


        private async Task<ApplicationUser> SaveUser(SignupModel signUp, string role)
        {
            var user = _mapper.Map<ApplicationUser>(signUp);

            var result = await _userManager.CreateAsync(user, signUp.Password);
            if (!result.Succeeded) throw new Exception("problem to create a user " +  JsonSerializer.Serialize( result.Errors));

            var createdUser = await _userManager.FindByNameAsync(signUp.Name);
            if (createdUser == null) throw new Exception("Cannot find the user after saving it"); // LOL what??

            await _userManager.AddToRoleAsync(createdUser, signUp.Role);

            return createdUser;
        }


        private string generateToken(ApplicationUser user, string role)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudiences"],
                expires: DateTime.Now.AddDays(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class SignupServiceResponse
    {
        public bool UserIsAlreadyExist { get; set; }
    }
}

