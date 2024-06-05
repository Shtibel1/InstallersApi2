using AutoMapper;
using BLL.DTOs;
using BLL.Models;
using BLL.Services.AuthService;
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

            CreateMap<CreateAssignmentVm, Assignment >()
                .AfterMap((src, dest) =>
                {
                    dest.CustomerId = Guid.NewGuid();    
                })
                .ReverseMap();

            CreateMap<Assignment, AssignmentVm>()
                .AfterMap((src, dest) =>
                {
                    dest.ServiceProvider = new ServiceProviderVm() { Id = src.ServiceProviderId };
                });

            CreateMap<ServiceProvider, ServiceProviderVm>().ReverseMap();


            CreateMap<Category, CategoryVm>().ReverseMap();

/*            CreateMap<ServiceProvider, ServiceProviderVm>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ServiceProviderCategories.Select(spc => spc.Category)))
            .ReverseMap()
            .ForMember(dest => dest.ServiceProviderCategories, opt => opt.MapFrom(src => src.Categories.Select(c => new ServiceProviderCategories { CategoryId = c.Id ?? Guid.NewGuid() })));*/
            
            


            CreateMap<ServiceProviderPricing, ServiceProviderPricingVm>().ReverseMap();
            CreateMap<Product, ProductVm>().ReverseMap();

            CreateMap<Marketer, MarketerVm>().ReverseMap();




            //creation

            CreateMap<CreateServiceProviderVm, ServiceProvider>()
                .AfterMap((src, dest) =>
                {
                    dest.Id = Guid.NewGuid();
                })
                .ReverseMap();

            CreateMap<SignupModel, AppUser>()
                .ForMember(des => des.UserName, src => src.MapFrom(act => act.Name))
                .ForMember(des => des.PhoneNumber, src => src.MapFrom(act => act.Phone));

            CreateMap<SignupModel, Employee>();

            CreateMap<SignupModel, CreateServiceProviderVm>()
            .ForMember(dest => dest.Categories, opt => opt.Condition(src => src.Role == Role.ServiceProvider && src.Categories != null));

            CreateMap<CreateServiceProviderVm, ServiceProvider>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Role.ServiceProvider.ToString()))
            .ForMember(dest => dest.Companies, opt => opt.Ignore());


            CreateMap<Company, CompanyVm>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom<CompanyNameResolver>());
        }
    }

    public class CompanyNameResolver : IValueResolver<Company, CompanyVm, CompanyNames>
    {
        public CompanyNames Resolve(Company source, CompanyVm destination, CompanyNames destMember, ResolutionContext context)
        {
            return Enum.TryParse<CompanyNames>(source.Name, out var result) ? result : CompanyNames.Unkown;
        }
    }
}
