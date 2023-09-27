using HonbunNoAnkiApi.DBContext;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories
{
    public class MeaningRepo : GenericRepo<Meaning>, IMeaningRepo
    {
        public MeaningRepo(MyDBContext context) : base(context)
        {
        }

        public async Task<Meaning> GetMeaning(long id)
        {
            return await _dbContext.Meanings.FindAsync(id);
        }

        public async Task<IEnumerable<Meaning>> GetMeanings()
        {
            return await _dbContext.Meanings.ToListAsync();
        }
    }
}
