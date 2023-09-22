using HonbunNoAnkiApi.DBContext;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories
{
    public class MeaningReadingRepo : GenericRepo<MeaningReading>, IMeaningReadingRepo
    {
        public MeaningReadingRepo(MyDBContext context) : base(context)
        {

        }

        public async Task<MeaningReading> GetMeaningReading(long id)
        {
            var meaningReading = await _dbContext.MeaningReadings
                .Where(s => s.MeaningReading_ID == id)
                .SingleOrDefaultAsync();
            return meaningReading;
        }

        public async Task<IEnumerable<MeaningReading>> GetMeaningReadings()
        {
            return await _dbContext.MeaningReadings.ToListAsync();
        }
    }
}
