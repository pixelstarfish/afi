﻿using Afi.CustomerPortal.Entities.Domain;
using Afi.CustomerPortal.Entities.Dto;
using AutoMapper;

namespace Afi.CustomerPortal.Configuration
{
    /// <summary>
    /// Mapping rules for entities.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerRegistration, Customer>()
                .ForMember(dest => dest.EmailAddress, opt => 
                    opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.EmailAddress) ? src.EmailAddress.ToLowerInvariant() : null))
                .ForMember(dest => dest.Policies, opt => 
                    opt.MapFrom(src => new List<CustomerPolicy> { new() { PolicyNumber = src.PolicyNumber } }));
        }
    }
}