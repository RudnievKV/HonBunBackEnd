using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DictionaryApi.Dtos.KanjiDtos
{
    public record KanjiEntryDto
    {
        public string OriginalEntry { get; init; }
        public KanjiDto? KanjiDto { get; init; }
    }

}
