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
            CreateMap<Comment, CommentVm>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.WorkerId))
                .ReverseMap();
            CreateMap<Customer, CustomerVm>().ReverseMap();

            CreateMap<CreateAssignmentVm, Assignment>()
                .ForMember(dest => dest.ServiceProviderIdExt, opt => opt.MapFrom(src => src.ServiceProviderId));

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

            CreateMap<CalaulationVm, Calculation>().ReverseMap();

            CreateMap<ServiceProductVm, ServiceProduct>().ReverseMap();
            CreateMap<ServiceProviderStockVm, ServiceProviderStock>().ReverseMap();
            CreateMap<ServiceProviderStockAuditVm, ServiceProviderStockAudit>().ReverseMap();

        }
    }
}