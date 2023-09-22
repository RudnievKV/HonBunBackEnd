using HonbunNoAnkiApi.DBContext;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories
{
    public class WordRepo : GenericRepo<Word>, IWordRepo
    {
        public WordRepo(MyDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Word> GetWord(long id)
        {
            var word = await _dbContext.Words
                .Include(s => s.MeaningReadings)
                .Include(s => s.Stage)
                .Where(s => s.Word_ID == id)
                .SingleOrDefaultAsync();
            return word;
        }

        public async Task<IEnumerable<Word>> GetWords()
        {
            var words = await _dbContext.Words
                .Include(s => s.MeaningReadings)
                .Include(s => s.Stage).ToListAsync();
            return words;
        }
    }
}
