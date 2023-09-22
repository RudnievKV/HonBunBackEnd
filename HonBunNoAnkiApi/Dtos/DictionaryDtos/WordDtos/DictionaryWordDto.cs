using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.DictionaryDtos.WordDtos
{
    public record DictionaryWordDto
    {
        public string Id { get; init; }
        public IEnumerable<WordKanjiElementDto> KanjiElementsDto { get; init; }
        public IEnumerable<WordReadingElementDto> ReadingElementsDto { get; init; }
        public IEnumerable<SenseDto> SensesDto { get; init; }
    }
}
