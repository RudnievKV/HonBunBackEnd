using HonbunNoAnkiApi.Dtos.MeaningReadingDtos;
using HonbunNoAnkiApi.Dtos.StageDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services.Interfaces
{
    public interface IMeaningReadingService
    {
        public Task<MeaningReadingDto> GetMeaningReading(long id);
        public Task<IEnumerable<MeaningReadingDto>> GetMeaningReadings();
        public Task<MeaningReadingDto> CreateMeaningReading(MeaningReadingCreateDto meaningReadingCreateDto);
        public Task<bool> DeleteMeaningReading(long id);
        public Task<MeaningReadingDto> UpdateMeaningReading(long id, MeaningReadingUpdateDto meaningReadingUpdateDto);
    }
}
