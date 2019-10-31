using AutoMapper;
using BigOferta.API.Dtos;
using BigOferta.API.Models;

namespace BigOferta.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<User, UserForDetailDto>();
        }
    }
}