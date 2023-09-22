using HonbunNoAnkiApi.Dtos.DictionaryDtos.KanjiDtos;
using HonbunNoAnkiApi.Models.DictionaryModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services.Interfaces
{
    public interface IDictionaryKanjiService
    {
        public Task<IEnumerable<KanjiEntryDto>> GetKanjis(Request request);
    }
}
