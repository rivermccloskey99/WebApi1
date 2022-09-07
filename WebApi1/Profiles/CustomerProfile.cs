using AutoMapper;
using WebApi1.Dtos;
using WebApi1.Models;

namespace WebApi1.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            // source -> target
            CreateMap<Customer, CustomerReadDto>();
            CreateMap<CustomerCreateDto, Customer>();
        }
    }
}