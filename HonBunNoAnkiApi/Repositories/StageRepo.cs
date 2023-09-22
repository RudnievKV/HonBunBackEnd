using HonbunNoAnkiApi.DBContext;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Repositories
{
    public class StageRepo : GenericRepo<Stage>, IStageRepo
    {
        public StageRepo(MyDBContext context) : base(context)
        {
        }

        public async Task<Stage> GetStage(long id)
        {
            var stage = await _dbContext.Stages
                .Where(s => s.Stage_ID == id)
                .SingleOrDefaultAsync();
            return stage;
        }

        public async Task<IEnumerable<Stage>> GetStages()
        {
            return await _dbContext.Stages.ToListAsync();
        }
    }
}
