using HonbunNoAnkiApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories.Interfaces
{
    public interface IWordCollectionRepo : IGenericRepo<WordCollection>
    {
        public Task<WordCollection> GetWordCollection(long id);
        public Task<IEnumerable<WordCollection>> GetWordCollections();
    }
}
