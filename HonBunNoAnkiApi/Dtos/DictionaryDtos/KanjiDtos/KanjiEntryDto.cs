using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HonbunNoAnkiApi.Dtos.DictionaryDtos.KanjiDtos
{
    public record KanjiEntryDto
    {
        public string OriginalEntry { get; init; }
        public KanjiDto KanjiDto { get; init; }
    }

}
