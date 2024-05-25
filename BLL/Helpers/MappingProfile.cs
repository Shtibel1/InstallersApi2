using AutoMapper;
using BLL.DTOs;
using BLL.Models;
using BLL.Services.AuthService;
using DAL.Abstracts;
using DAL.Entities;

namespace DAL.Helpers
{
    public class MappingProfile : Profile   
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentVm>().ReverseMap();

            CreateMap<Customer, CustomerVm>().ReverseMap();

            CreateMap<CreateAssignmentVm, Assignment >()
                .AfterMap((src, dest) =>
                {
                    dest.CustomerId = Guid.NewGuid();    
                })
                .ReverseMap();

            CreateMap<Assignment, AssignmentVm>().ReverseMap();


            CreateMap<Category, CategoryVm>().ReverseMap();

/*            CreateMap<ServiceProvider, ServiceProviderVm>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ServiceProviderCategories.Select(spc => spc.Category)))
            .ReverseMap()
            .ForMember(dest => dest.ServiceProviderCategories, opt => opt.MapFrom(src => src.Categories.Select(c => new ServiceProviderCategories { CategoryId = c.Id ?? Guid.NewGuid() })));*/
            
            CreateMap<SignupModel, AppUser>()
                .ForMember(des => des.UserName, src => src.MapFrom(act => act.Name))
                .ForMember(des => des.PhoneNumber, src => src.MapFrom(act => act.Phone));

            CreateMap<SignupModel, Employee>();

            CreateMap<SignupModel, ServiceProvider>();
            CreateMap<Company, CompanyVm>().ReverseMap();


            CreateMap<ServiceProviderPricing, ServiceProviderPricingVm>().ReverseMap();
            CreateMap<Product, ProductVm>().ReverseMap();
                

            




        }
    }
}
