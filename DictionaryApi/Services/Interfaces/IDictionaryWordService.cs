using DictionaryApi.Dtos.WordDtos;
using DictionaryApi.Models;
using DictionaryApi.Models.WordModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DictionaryApi.Services.Interfaces
{
    public interface IDictionaryWordService
    {
        public Task<IEnumerable<WordNameEntryDto>> GetWords(Request request);

    }
}
