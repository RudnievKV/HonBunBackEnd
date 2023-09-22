using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories.Interfaces
{
    public interface IUserRepo : IGenericRepo<User>
    {
        public Task<User> GetUser(long id);
        public Task<IEnumerable<User>> GetUsers();
    }
}
