using HonbunNoAnki.Models;
using HonbunNoAnkiApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories.Interfaces
{
    public interface IMeaningReadingRepo : IGenericRepo<MeaningReading>
    {
        public Task<MeaningReading> GetMeaningReading(long id);
        public Task<IEnumerable<MeaningReading>> GetMeaningReadings();
    }
}
