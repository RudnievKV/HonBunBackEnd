using DictionaryApi.Models.WordModels;
using System.Collections.Generic;

namespace DictionaryApi.Dtos.WordDtos
{
    public record WordDto
    {
        public string Id { get; init; }
        public IEnumerable<WordKanjiElementDto>? KanjiElementsDto { get; init; }
        public IEnumerable<WordReadingElementDto> ReadingElementsDto { get; init; }
        public IEnumerable<SenseDto> SensesDto { get; init; }
    }
}
