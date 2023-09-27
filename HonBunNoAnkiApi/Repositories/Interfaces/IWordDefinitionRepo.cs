using HonbunNoAnkiApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories.Interfaces
{
    public interface IWordDefinitionRepo : IGenericRepo<WordDefinition>
    {
        public Task<WordDefinition> GetWordDefinition(long id);
        public Task<IEnumerable<WordDefinition>> GetWordDefinitions();
    }
}
