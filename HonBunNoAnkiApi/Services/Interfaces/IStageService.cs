using HonbunNoAnkiApi.Dtos.StageDtos;
using HonbunNoAnkiApi.Dtos.UserDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services.Interfaces
{
    public interface IStageService
    {
        public Task<StageDto> GetStage(long id);
        public Task<IEnumerable<StageDto>> GetStages();
        public Task<StageDto> CreateStage(StageCreateDto stageCreateDto);
        public Task<bool> DeleteStage(long id);
        public Task<StageDto> UpdateStage(long id, StageUpdateDto stageUpdateDto);
    }
}
