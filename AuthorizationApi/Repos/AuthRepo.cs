using AuthorizationApi.DBContext;
using AuthorizationApi.Models;
using AuthorizationApi.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace AuthorizationApi.Repos
{
    public class AuthRepo : IAuthRepo
    {
        private MyDBContext _dbContext;

        public AuthRepo(MyDBContext context)
        {
            _dbContext = context;
        }

        public async Task<User> GetUser(string email, string password)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(s => s.Email == email);
            return user;
        }

        public async Task Register(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
