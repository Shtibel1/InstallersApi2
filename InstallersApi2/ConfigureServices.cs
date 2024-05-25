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

            services.AddScoped<IEmployeesRepository, ManagersRepository>();
            services.AddScoped<IServiceProviderRepository, ServiceProviderRepository>();
            services.AddScoped<IAssignmentsRepository, AssignmentsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IServiceProviderPricingRepository, ServiceProviderPricingRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();

            services.AddScoped<IManagersService, ManagersService>();
            services.AddScoped<IServiceProvidersService, ServiceProvidersService>();
            services.AddScoped<IAssignmentsService, AssignmentsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IServiceProviderPricingService, ServiceProviderPricingService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddSingleton<WebSocketService>();

            services.AddScoped<ICompanyDataProvider, CompanyDataProvider>();


            
            /*services.AddScoped(provider =>
            {
                return new CompanyDbContext("Data Source=DESKTOP-9C5JK3S\\SQLEXPRESS;Initial Catalog=Shtibay;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            });*/


/*            services.AddScoped(provider =>
            {
                var connectionStringProvider = provider.GetRequiredService<ICompanyDataProvider>();
                var connectionStrings = connectionStringProvider.GetConnectionStrings();


                return new CompanyDbContext(connectionString, connectionStringProvider);
            });*/

        }
    }
}
