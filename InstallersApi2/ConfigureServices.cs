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

            

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });



           
            services.AddHttpClient();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.AllowedUserNameCharacters = "אבגדהוזחטיכלמנסעפצקרשתךםןףץabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
            })
                .AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

            services.AddScoped<IManagersRepository, ManagersRepository>();
            services.AddScoped<IInstallersRepository, InstallersRepository>();
            services.AddScoped<IAssignmentsRepository, AssignmentsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IInstallerPricingRepository, InstallerPricingRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();

            services.AddScoped<IManagersService, ManagersService>();
            services.AddScoped<IInstallersService, InstallersService>();
            services.AddScoped<IAssignmentsService, AssignmentsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IInstallerPricingService, InstallerPricingService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IAuthService, AuthService>();



        }
    }
}
