using AutoMapper;
using HonbunNoAnkiApi.Dtos.MeaningReadingDtos;
using HonbunNoAnkiApi.Dtos.StageDtos;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services
{
    public class MeaningReadingService : IMeaningReadingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MeaningReadingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<MeaningReadingDto> GetMeaningReading(long id)
        {
            var meaningReading = await _unitOfWork.MeaningReadingRepo.GetMeaningReading(id);
            var meaningReadingDto = _mapper.Map<MeaningReadingDto>(meaningReading);
            return meaningReadingDto;
        }

        public async Task<IEnumerable<MeaningReadingDto>> GetMeaningReadings()
        {
            var meaningReadings = await _unitOfWork.MeaningReadingRepo.GetMeaningReadings();
            var meaningReadingDtos = new List<MeaningReadingDto>();
            foreach (var meaningReading in meaningReadings)
            {
                var meaningReadingDto = _mapper.Map<MeaningReadingDto>(meaningReading);
                meaningReadingDtos.Add(meaningReadingDto);
            }
            return meaningReadingDtos;
        }
        public async Task<MeaningReadingDto> CreateMeaningReading(MeaningReadingCreateDto meaningReadingCreateDto)
        {
            var newMeaningReading = new MeaningReading()
            {
                Meaning = meaningReadingCreateDto.Meaning,
                PartOfSpeech = meaningReadingCreateDto.PartOfSpeech,
                Reading = meaningReadingCreateDto.Reading,
                Word_ID = meaningReadingCreateDto.Word_ID,
                OriginalEntry = meaningReadingCreateDto.OriginalEntry,
            };
            _unitOfWork.MeaningReadingRepo.Create(newMeaningReading);
            await _unitOfWork.SaveChangesAsync();

            var meaningReadingDto = await GetMeaningReading(newMeaningReading.MeaningReading_ID);
            return meaningReadingDto;
        }

        public async Task<bool> DeleteMeaningReading(long id)
        {
            var meaningReading = await _unitOfWork.MeaningReadingRepo.GetMeaningReading(id);
            if (meaningReading == null)
            {
                return false;
            }

            _unitOfWork.MeaningReadingRepo.Delete(meaningReading);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<MeaningReadingDto> UpdateMeaningReading(long id, MeaningReadingUpdateDto meaningReadingUpdateDto)
        {
            var meaningReading = await _unitOfWork.MeaningReadingRepo
                .Find(s => s.MeaningReading_ID == id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (meaningReading == null)
            {
                return null;
            }


            var newMeaningReading = new MeaningReading()
            {
                Meaning = meaningReadingUpdateDto.Meaning,
                PartOfSpeech = meaningReadingUpdateDto.PartOfSpeech,
                Reading = meaningReadingUpdateDto.Reading,
                Word_ID = meaningReadingUpdateDto.Word_ID,
                MeaningReading_ID = id,
                OriginalEntry = meaningReadingUpdateDto.OriginalEntry,
            };

            _unitOfWork.MeaningReadingRepo.Update(newMeaningReading);
            await _unitOfWork.SaveChangesAsync();


            var meaningReadingDto = await GetMeaningReading(id);
            return meaningReadingDto;
        }
    }
}
