using AutoMapper;
using MultiLib.Auth.Dtos.User;
using MultiLib.Auth.models;

namespace MultiLib.Auth
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
           
            CreateMap<AuthUserDto, User>();
            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, GetUserDto>();
        }
    }
}
