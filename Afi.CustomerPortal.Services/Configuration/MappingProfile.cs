using Afi.CustomerPortal.Entities.Domain;
using Afi.CustomerPortal.Entities.Dto;
using AutoMapper;

namespace Afi.CustomerPortal.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerRegistration, Customer>()
                .ForMember(dest => dest.Policies, opt => 
                    opt.MapFrom(src => new List<CustomerPolicy> { new() { PolicyNumber = src.PolicyNumber } }));
        }
    }
}