using HonbunNoAnkiApi.DBContext;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories
{
    public class WordCollectionRepo : GenericRepo<WordCollection>, IWordCollectionRepo
    {
        public WordCollectionRepo(MyDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<WordCollection> GetWordCollection(long id)
        {
            var wordCollection = await _dbContext.WordCollections
               .Include(s => s.User)
               .Include(s => s.Words).ThenInclude(s => s.WordDefinitions)
               .Include(s => s.Words).ThenInclude(s => s.Stage)
               .Where(s => s.WordCollection_ID == id)
               .SingleOrDefaultAsync();
            return wordCollection;
        }

        public async Task<IEnumerable<WordCollection>> GetWordCollections()
        {
            var wordCollections = await _dbContext.WordCollections
                            .Include(s => s.User)
                            .Include(s => s.Words).ThenInclude(s => s.WordDefinitions).ThenInclude(s => s.Reading)
                            .Include(s => s.Words).ThenInclude(s => s.WordDefinitions).ThenInclude(s => s.Meanings).ThenInclude(s => s.MeaningValues)
                            .Include(s => s.Words).ThenInclude(s => s.Stage)
                            .ToListAsync();
            return wordCollections;
        }
    }
}
