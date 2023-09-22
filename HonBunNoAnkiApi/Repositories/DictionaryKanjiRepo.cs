using HonbunNoAnkiApi.Dtos.DictionaryDtos.KanjiDtos;
using HonbunNoAnkiApi.Models.DictionaryModels;
using HonbunNoAnkiApi.Models.DictionaryModels.KanjiModels;
using HonbunNoAnkiApi.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories
{
    public class DictionaryKanjiRepo : IDictionaryKanjiRepo
    {
        private readonly IMongoCollection<Kanji> _kanji;
        public DictionaryKanjiRepo(IDBSettings settings, MongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            this._kanji = database.GetCollection<Kanji>(settings.KanjiCollectionName);
        }

        public async Task<IAsyncCursor<Kanji>> GetKanjis(string filter)
        {
            return await _kanji.FindAsync(filter);
        }
    }
}
