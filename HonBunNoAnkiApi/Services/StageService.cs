using AutoMapper;
using HonbunNoAnki.DBContext;
using HonbunNoAnki.Models;
using HonbunNoAnkiApi.Dtos.StageDtos;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services
{
    public class StageService : IStageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<StageDto> GetStage(long id)
        {
            var stage = await _unitOfWork.StageRepo.GetStage(id);
            var stageDto = _mapper.Map<StageDto>(stage);
            return stageDto;
        }
        public async Task<IEnumerable<StageDto>> GetStages()
        {
            var stages = await _unitOfWork.StageRepo.GetStages();
            var stageDtos = new List<StageDto>();
            foreach (var stage in stages)
            {
                var stageDto = _mapper.Map<StageDto>(stage);
                stageDtos.Add(stageDto);
            }
            return stageDtos;
        }
        public async Task<StageDto> CreateStage(StageCreateDto stageCreateDto)
        {
            var newStage = new Stage()
            {
                Duration = stageCreateDto.Duration,
                StageName = stageCreateDto.StageName,
                StageNumber = stageCreateDto.StageNumber,
            };
            _unitOfWork.StageRepo.Create(newStage);
            await _unitOfWork.SaveChangesAsync();

            var stageDto = await GetStage(newStage.Stage_ID);
            return stageDto;
        }

        public async Task<bool> DeleteStage(long id)
        {
            var stage = await _unitOfWork.StageRepo.GetStage(id);
            if (stage == null)
            {
                return false;
            }

            _unitOfWork.StageRepo.Delete(stage);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        public async Task<StageDto> UpdateStage(long id, StageUpdateDto stageUpdateDto)
        {
            var stage = await _unitOfWork.StageRepo
                .Find(s => s.Stage_ID == id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (stage == null)
            {
                return null;
            }


            var newStage = new Stage()
            {
                Duration = stageUpdateDto.Duration,
                StageName = stageUpdateDto.StageName,
                StageNumber = stageUpdateDto.StageNumber,
                Stage_ID = id
            };
            _unitOfWork.StageRepo.Update(newStage);
            await _unitOfWork.SaveChangesAsync();


            var stageDto = await GetStage(id);
            return stageDto;
        }
    }
}
