using AutoMapper;
using CodeFirstPractice.DTOs.Request;
using CodeFirstPractice.DTOs.Response;
using CodeFirstPractice.Models;

namespace CodeFirstPractice.MappingProfiles
{
    public class CustomerMappingProfiles : Profile
    {
        public CustomerMappingProfiles()
        {
            CreateMap<Customer, CreateCustomerRequest>();

            CreateMap<CreateCustomerProfileRequest, CustomerProfile>();

            CreateMap<CreateCustomerRequest, Customer>();

            CreateMap<Customer, CustomerResponse>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Profile.Address));
        }
    }
}
