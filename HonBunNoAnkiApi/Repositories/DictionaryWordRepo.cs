using HonbunNoAnkiApi.DBContext;
using HonbunNoAnkiApi.Models.DictionaryModels;
using HonbunNoAnkiApi.Models.DictionaryModels.KanjiModels;
using HonbunNoAnkiApi.Models.DictionaryModels.NameModels;
using HonbunNoAnkiApi.Models.DictionaryModels.WordModels;
using HonbunNoAnkiApi.Repositories.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories
{
    public class DictionaryWordRepo : IDictionaryWordRepo
    {
        private readonly IMongoCollection<Name> _names;
        private readonly IMongoCollection<DictionaryWord> _words;
        public DictionaryWordRepo(IDBSettings settings, MongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _names = database.GetCollection<Name>(settings.NamesCollectionName);
            _words = database.GetCollection<DictionaryWord>(settings.WordsCollectionName);
        }

        public async Task<IAsyncCursor<Name>> GetNames(string filter)
        {
            return await _names.FindAsync(filter);
        }

        public async Task<IAsyncCursor<DictionaryWord>> GetWords(string filter)
        {
            return await _words.FindAsync(filter);
        }
    }
}
