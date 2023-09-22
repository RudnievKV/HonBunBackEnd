using AuthorizationApi.Models;
using System;
using System.Threading.Tasks;
namespace AuthorizationApi.Repos.Interfaces
{
    public interface IAuthRepo
    {
        public Task<User> GetUser(string email, string password);
        public Task Register(User user);
    }
}
