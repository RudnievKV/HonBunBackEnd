using AutoMapper;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Models;

namespace HonbunNoAnkiApi.Dtos.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
