using HonbunNoAnkiApi.Dtos.DictionaryDtos.WordDtos;
using HonbunNoAnkiApi.Models.DictionaryModels.NameModels;
using HonbunNoAnkiApi.Models.DictionaryModels.WordModels;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories.Interfaces
{
    public interface IDictionaryWordRepo
    {
        public Task<IAsyncCursor<DictionaryWord>> GetWords(string filter);
        public Task<IAsyncCursor<Name>> GetNames(string filter);
    }
}
