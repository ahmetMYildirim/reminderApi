using AutoMapper;
using Entities.Auth_Models;
using Entities.Dto;
using Entities.Models;

namespace Reminde_API.Utilities
{
    public class AutoMapper : Profile
    {
        public AutoMapper() 
        {
            CreateMap<UserF_Reg, User>();
        }
    }
}
