using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HonbunNoAnkiApi.Dtos.DictionaryDtos.KanjiDtos
{

    public record KanjiDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Id { get; init; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Literal { get; init; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MiscellaniousDto Miscellanious { get; init; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<ReadingDto> Readings { get; init; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string> Meanings { get; init; }
        //public IEnumerable<ReadingMeaningDto> ReadingMeaning { get; init; }

        //public IEnumerable<string> Meanings { get; init; }

    }

}
