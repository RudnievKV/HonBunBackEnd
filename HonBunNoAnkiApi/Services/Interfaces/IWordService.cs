using HonbunNoAnkiApi.Dtos.DictionaryDtos.WordDtos;
using HonbunNoAnkiApi.Dtos.StageDtos;
using HonbunNoAnkiApi.Dtos.WordDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services.Interfaces
{
    public interface IWordService
    {
        public Task<WordDto> GetWord(long id);
        public Task<IEnumerable<WordDto>> GetWords();
        public Task<WordDto> CreateWord(WordCreateDto wordCreateDto);
        public Task<bool> DeleteWord(long id);
        public Task<WordDto> UpdateWord(long id, WordUpdateDto wordUpdateDto);
        public Task<IEnumerable<WordDto>> GetSRSWords(long userID);
        public Task<WordDto> UpdateWordBasedOnReview(long id, WordUpdateReviewDto wordUpdateReviewDto, long userID);
    }
}
