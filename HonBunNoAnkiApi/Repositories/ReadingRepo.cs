using HonbunNoAnkiApi.DBContext;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories
{
    public class ReadingRepo : GenericRepo<Reading>, IReadingRepo
    {
        public ReadingRepo(MyDBContext context) : base(context)
        {
        }

        public async Task<Reading> GetReading(long id)
        {
            return await _dbContext.Readings.FindAsync(id);
        }

        public async Task<IEnumerable<Reading>> GetReadings()
        {
            return await _dbContext.Readings.ToListAsync();
        }
    }
}
