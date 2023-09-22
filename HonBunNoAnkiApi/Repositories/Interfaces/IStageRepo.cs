using HonbunNoAnkiApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories.Interfaces
{
    public interface IStageRepo : IGenericRepo<Stage>
    {
        public Task<Stage> GetStage(long id);
        public Task<IEnumerable<Stage>> GetStages();
    }
}
