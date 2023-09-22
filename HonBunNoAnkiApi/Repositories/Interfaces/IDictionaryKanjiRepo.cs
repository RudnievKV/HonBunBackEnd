using HonbunNoAnkiApi.Dtos.DictionaryDtos.KanjiDtos;
using HonbunNoAnkiApi.Models.DictionaryModels;
using HonbunNoAnkiApi.Models.DictionaryModels.KanjiModels;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories.Interfaces
{
    public interface IDictionaryKanjiRepo
    {
        public Task<IAsyncCursor<Kanji>> GetKanjis(string filter);
    }
}
