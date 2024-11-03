using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Models;
using Business;
using Business.Models;
using DAL.Abstracts;
using DAL.Entities;
using DAL.Enums;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace BLL.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _loginManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IServiceProvidersRepository _serviceProvidersRepo;
        private readonly ISPService _serviceProvidersService;

        private readonly ICategoriesRepository _categoriesRepository;

        public AuthService(
           UserManager<AppUser> userManager,
           RoleManager<AppRole> roleManager,
           SignInManager<AppUser> loginManager,
           IConfiguration configuration,
           IMapper mapper,
           IEmployeesRepository employeesRepository,
           ISPService serviceProvidersService,
           ICategoriesRepository categoriesRepository,
           IServiceProvidersRepository serviceProvidersRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _loginManager = loginManager;
            _configuration = configuration;
            _mapper = mapper;
            _employeesRepository = employeesRepository;
            _serviceProvidersService = serviceProvidersService;
            _categoriesRepository = categoriesRepository;
            _serviceProvidersRepo = serviceProvidersRepo;
        }

        public async Task<AppUserVm?> LoginAsync(LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null) return null;

            var signInResult = await _loginManager.PasswordSignInAsync(user.UserName, login.Password, false, false);
            if (!signInResult.Succeeded) return null;

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0) return null;


            var role = Enum.Parse<Role>(roles[0]);

            Guid userId;
            var token = string.Empty;
            var companiesVm = new List<CompanyVm>();

            switch (role)
            {
                case Role.Employee:
                case Role.Storekeeper:
                    var employee = await _employeesRepository.GetEmployeeByUserId(user.Id);
                    userId = employee.Id;
                    companiesVm = _mapper.Map<List<CompanyVm>>(employee.Companies);
                    token = GenerateToken(user, role, employee.Companies.Select(c => c.Name).ToList());
                    break;
                case Role.ServiceProvider:
                    var serviceProvider = await _serviceProvidersRepo.GetserviceProviderbyUserId(user.Id);
                    userId = serviceProvider.Id;
                    companiesVm = _mapper.Map<List<CompanyVm>>(serviceProvider.Companies);
                    token = GenerateToken(user, role, serviceProvider.Companies.Select(n => n.Name).ToList());
                    break;
                default:
                    return null;
            }




            return new AppUserVm { Id = userId, Name = user.UserName, Token = token, Role = Enum.Parse<Role>(roles[0]), Companies = companiesVm };
        }

        public async Task<SignupServiceResponse?> SignupAsync(SignupModel signUp)
        {
            var user = await _userManager.FindByNameAsync(signUp.Name);
            if (user != null) return new SignupServiceResponse { UserIsAlreadyExist = true };

            var savedUser = await SaveUser(signUp, signUp.Role);
            var savedServiceProvider = await SaveRoleSpecificDataAsync(signUp, savedUser.Id);

            return new SignupServiceResponse { UserIsAlreadyExist = false };
        }

        private async Task<object> SaveRoleSpecificDataAsync(SignupModel signUp, Guid identityId)
        {
            switch (signUp.Role)
            {
                case Role.Employee:
                case Role.Storekeeper:
                    var managerEntity = _mapper.Map<Employee>(signUp);
                    managerEntity.IdentityId = identityId;
                    return await _employeesRepository.CreateEmployeeAsync(managerEntity);

                case Role.ServiceProvider:
                    var createServiceProvider = _mapper.Map<CreateServiceProviderVm>(signUp);
                    createServiceProvider.IdentityId = identityId;
                    var createdServiceProvider = await _serviceProvidersService.CreateServiceProviderAsync(createServiceProvider);
                    return createdServiceProvider;

                default:
                    
                    throw new Exception("Role not found in SignupModel");
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        private async Task<AppUser> SaveUser(SignupModel signUp, Role role)
        {
            var user = new AppUser
            {
                UserName = signUp.Name,
            };
            var result = await _userManager.CreateAsync(user, signUp.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Problem creating user: " + JsonSerializer.Serialize(result.Errors));
            }

            var createdUser = await _userManager.FindByNameAsync(signUp.Name);
            if (createdUser == null)
            {
                throw new Exception("Cannot find the user after saving it");
            }

            await _userManager.AddToRoleAsync(createdUser, role.ToString());
            return createdUser;
        }

        private string GenerateToken(AppUser user, Role role ,List<string>? companyNames)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(CustomClaimTypes.Companies, JsonSerializer.Serialize(companyNames)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = SetTokenConfig(authClaims);
            return token;
        }

        private string SetTokenConfig(List<Claim> authClaims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(int.Parse(_configuration["JWT:TokenExpirationDays"])),
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

