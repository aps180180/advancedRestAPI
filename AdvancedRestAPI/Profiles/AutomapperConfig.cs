using AdvancedRestAPI.DTOs;
using AdvancedRestAPI.Models;
using AutoMapper;

namespace AdvancedRestAPI.Profiles
{
    public class AutomapperConfig:Profile
    {
        public AutomapperConfig() 
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
           
        }
    }
}
