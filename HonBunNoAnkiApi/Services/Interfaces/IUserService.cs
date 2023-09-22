using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using HonbunNoAnkiApi.Dtos.UserDtos;

namespace HonbunNoAnkiApi.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetUser(long id);
        public Task<IEnumerable<UserDto>> GetUsers();
        public Task<UserDto> CreateUser(UserCreateDto userCreateDto);
        public Task DeleteUser(long id);
        public Task<UserDto> UpdateUser(long id, UserUpdateDto userUpdateDto);
    }
}
