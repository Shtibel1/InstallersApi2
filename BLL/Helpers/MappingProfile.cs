using AutoMapper;
using BLL.DTOs;
using BLL.Models;
using BLL.Services.AuthService;
using BLL.Vms;
using DAL.Abstracts;
using DAL.Entities;
using DAL.Enums;

namespace DAL.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentVm>().ReverseMap();
            CreateMap<Customer, CustomerVm>().ReverseMap();

            CreateMap<CreateAssignmentVm, Assignment>().ReverseMap();

            CreateMap<Assignment, AssignmentVm>()
                .AfterMap((src, dest) =>
                {
                    dest.ServiceProvider = new ServiceProviderVm() { Id = src.ServiceProviderIdExt };
                });

            CreateMap<ServiceProvider, ServiceProviderVm>().ReverseMap();
            CreateMap<Category, CategoryVm>().ReverseMap();
            CreateMap<Additional, AdditionalVm>().ReverseMap();

            CreateMap<SignupModel, Employee>().ReverseMap();
            CreateMap<CreateServiceProviderVm, SignupModel>();
            CreateMap<Company, CompanyVm>().ReverseMap();
            CreateMap<ProductVm, Product>().ReverseMap();

            CreateMap<CreateServiceProviderVm, SignupModel>().ReverseMap();
            CreateMap<CreateServiceProviderVm, ServiceProviderVm>().ReverseMap();
            CreateMap<CreateServiceProviderVm, ServiceProvider>().ReverseMap();
            CreateMap<MarketerVm, Marketer>().ReverseMap();
            CreateMap<AdditionalPriceVm, AdditionalPrice>().ReverseMap();
            
        }
    }
}