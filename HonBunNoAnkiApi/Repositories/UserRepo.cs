using HonbunNoAnkiApi.DBContext;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(MyDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUser(long id)
        {
            var user = await _dbContext.Users
                .Where(s => s.User_ID == id)
                .SingleOrDefaultAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}
