using HonbunNoAnkiApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories.Interfaces
{
    public interface IWordRepo : IGenericRepo<Word>
    {
        public Task<Word> GetWord(long id);
        public Task<IEnumerable<Word>> GetWords();
    }
}
