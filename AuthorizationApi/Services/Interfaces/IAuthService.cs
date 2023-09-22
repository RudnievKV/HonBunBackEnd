using AuthorizationApi.Dtos;
using AuthorizationApi.Models;
using System.Threading.Tasks;

namespace AuthorizationApi.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<User> GetUser(string email, string password);
        public Task<UserDto> Register(RegisterForm user);
    }
}
