using HonbunNoAnkiApi.Models;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories.Interfaces
{
    public interface IMeaningValueRepo : IGenericRepo<MeaningValue>
    {
        public Task<MeaningValue> GetMeaningValue(long id);
        public Task<IEnumerable<MeaningValue>> GetMeaningValues();
    }
}
