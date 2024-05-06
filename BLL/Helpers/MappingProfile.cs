using AutoMapper;
using BLL.DTOs.Abstracts;
using BLL.Models;
using BLL.Services.AuthService;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Customer, CustomerVm>().ReverseMap();
            CreateMap<CreateAssignmentVm, Assignment >()
                /*.ForMember(des => des.Customer, opt => opt.MapFrom(src => src.CreatedDate))*/
                .AfterMap((src, dest) =>
                {
                    dest.CustomerId = 0;    
                })
                .ReverseMap();

            CreateMap<Installer, InstallerDto>()
            .IncludeBase<Worker, WorkerDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryInstallers.Select(ci => ci.Category)));
            CreateMap<Category, CategoryDto>().ReverseMap();




            CreateMap<Installer, InstallerDto>()
                .IncludeBase<Worker, WorkerDto>()
                .ForMember(des => des.Categories, src => src.MapFrom(act => act.CategoryInstallers));
                

            CreateMap<Manager, ManagerVm>()
                .IncludeBase<Worker, WorkerDto>()
                .ReverseMap();
            CreateMap<ManagerVm, Manager>()
                .IncludeBase<WorkerDto, Worker>()
                .ReverseMap();

            CreateMap<Worker, WorkerDto>().ReverseMap();


            CreateMap<SignupModel, ApplicationUser>()
                .ForMember(des => des.UserName, src => src.MapFrom(act => act.Name))
                .ForMember(des => des.PhoneNumber, src => src.MapFrom(act => act.Phone));

            CreateMap<SignupModel, Manager>();

            CreateMap<SignupModel, Installer>()
                .ForMember(des => des.CategoryInstallers, src => src.MapFrom(act => act.Categories.Select(c => new CategoryInstaller { CategoryId = c})));




            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Assignment, AssignmentVm>().ReverseMap();

            CreateMap<InstallerPricing, InstallerPricingVm>().ReverseMap();
            CreateMap<Product, ProductVm>().ReverseMap();

            CreateMap<CreateInstallerModel, Installer>();

            CreateMap<CategoryInstaller, CategoryDto>();

                
            /* CreateMap<ApplicationUser, WorkerDto>()
                 .ForMember(dst => dst.Name, src => src.MapFrom(act => act.UserName))
                 .ForMember(dst => dst.Phone, src => src.MapFrom(act => act.PhoneNumber));*/

        }
    }
}
