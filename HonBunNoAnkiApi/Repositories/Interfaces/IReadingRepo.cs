using HonbunNoAnkiApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories.Interfaces
{
    public interface IReadingRepo : IGenericRepo<Reading>
    {
        public Task<Reading> GetReading(long id);
        public Task<IEnumerable<Reading>> GetReadings();
    }
}
