using DictionaryApi.Dtos.KanjiDtos;
using DictionaryApi.Dtos.WordDtos;
using DictionaryApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DictionaryApi.Services.Interfaces
{
    public interface IDictionaryKanjiService
    {
        public Task<IEnumerable<KanjiEntryDto>> GetKanjis(Request request);
    }
}
