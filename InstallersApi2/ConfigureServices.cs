using AutoMapper;
using BLL.Interfaces;
using BLL.Services.AuthService;
using BLL.Services;
using Business;
using DAL.Data;
using DAL.Entities;
using DAL.Helpers;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using DAL.Providers;
using DAL.Repositories.Assignments;
using InstallersApi2.Controllers;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InstallersApi
{
    public class ConfigureServices
    {
        public static void AppConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddNewtonsoftJson();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddDbContext<CentralDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddHttpClient();



            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.AllowedUserNameCharacters = "אבגדהוזחטיכלמנסעפצקרשתךםןףץabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
            })
                .AddEntityFrameworkStores<CentralDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<IServiceProvidersRepository, ServiceProviderRepository>();
            services.AddScoped<IAssignmentsRepository, AssignmentsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();

            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<ISPService, ServiceProvidersService>();
            services.AddScoped<IAssignmentsService, AssignmentsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMarketersRepository, MarketersRepository>();
            services.AddScoped<IMarketersService, MarketersService>();
            services.AddScoped<IServiceProviderCategoryRepository, ServiceProviderCategoryRepository>();
            services.AddScoped<IAdditionalsService, AdditionalsService>();
            services.AddScoped<IAdditionalsRepository, AdditionalsRepository>();
            services.AddScoped<IAdditionalPriceRepository, AdditionalPriceRepository>();
            services.AddScoped<IAdditionalPriceService, AdditionalPriceService>();
            services.AddSingleton<WebSocketService>();

            services.AddScoped<ICompanyDataProvider, CompanyDataProvider>();

            services.AddScoped<CompanyDbContext>(provider =>
            {
                return new CompanyDbContext("Data Source=faults.c6v1xirlb8ux.eu-west-1.rds.amazonaws.com;Initial Catalog=Shtibay;User ID=admin;Password=Nads9Nads9; Encrypt=False; TrustServerCertificate=False");
            });

            //services.AddDbContext<CompanyDbContext>();
        }
    }
}
