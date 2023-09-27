using HonbunNoAnkiApi.DBContext;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories
{
    public class MeaningValueRepo : GenericRepo<MeaningValue>, IMeaningValueRepo
    {
        public MeaningValueRepo(MyDBContext context) : base(context)
        {
        }

        public async Task<MeaningValue> GetMeaningValue(long id)
        {
            return await _dbContext.MeaningValues.FindAsync(id);
        }

        public async Task<IEnumerable<MeaningValue>> GetMeaningValues()
        {
            return await _dbContext.MeaningValues.ToListAsync();
        }
    }
}
