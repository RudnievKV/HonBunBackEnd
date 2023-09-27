using HonbunNoAnkiApi.Dtos.StageDtos;
using HonbunNoAnkiApi.Dtos.WordDefinitionDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services.Interfaces
{
    public interface IWordDefinitionService
    {
        public Task<WordDefinitionDto> GetWordDefinition(long id);
        public Task<IEnumerable<WordDefinitionDto>> GetWordDefinitions();
        public Task<WordDefinitionDto> CreateWordDefinition(WordDefinitionCreateDto meaningReadingCreateDto);
        public Task<bool> DeleteWordDefinition(long id);
        public Task<WordDefinitionDto> UpdateWordDefinition(long id, WordDefinitionUpdateDto meaningReadingUpdateDto);
    }
}
