using HonbunNoAnkiApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories.Interfaces
{
    public interface IMeaningRepo : IGenericRepo<Meaning>
    {
        public Task<Meaning> GetMeaning(long id);
        public Task<IEnumerable<Meaning>> GetMeanings();
    }
}
