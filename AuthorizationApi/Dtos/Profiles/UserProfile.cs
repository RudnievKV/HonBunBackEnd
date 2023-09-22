using AuthorizationApi.Models;
using AutoMapper;

namespace AuthorizationApi.Dtos.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
