using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Dtos.WordCollectionDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services.Interfaces
{
    public interface IWordCollectionService
    {
        public Task<WordCollectionDto> GetWordCollection(long id);
        public Task<IEnumerable<WordCollectionDto>> GetWordCollections(long userID);
        public Task<IEnumerable<WordCollectionDto>> GetUserWordCollections(long id);
        public Task<WordCollectionDto> CreateWordCollection(WordCollectionCreateDto wordCollectionCreateDto);
        public Task<WordCollectionDto> CreateWordsFromText(string text, long wordCollectionID);
        public Task<bool> DeleteWordCollection(long id);
        public Task<WordCollectionDto> UpdateWordCollection(long id, WordCollectionUpdateDto wordCollectionUpdateDto);
        public Task<WordCollectionDto> CopyWordCollection(long wordCollectionID, long userID);
    }
}
