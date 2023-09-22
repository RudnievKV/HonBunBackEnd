using HonbunNoAnkiApi.Dtos.DictionaryDtos.WordDtos;
using HonbunNoAnkiApi.Models.DictionaryModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services.Interfaces
{
    public interface IDictionaryWordService
    {
        public Task<IEnumerable<WordNameEntryDto>> GetWords(Request request);

    }
}
