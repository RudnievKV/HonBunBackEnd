using HonbunNoAnkiApi.DBContext;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories
{
    public class WordDefinitionRepo : GenericRepo<WordDefinition>, IWordDefinitionRepo
    {
        public WordDefinitionRepo(MyDBContext context) : base(context)
        {

        }

        public async Task<WordDefinition> GetWordDefinition(long id)
        {
            var meaningReading = await _dbContext.WordDefinitions
                .Where(s => s.WordDefinition_ID == id)
                .SingleOrDefaultAsync();
            return meaningReading;
        }

        public async Task<IEnumerable<WordDefinition>> GetWordDefinitions()
        {
            return await _dbContext.WordDefinitions.ToListAsync();
        }
    }
}
