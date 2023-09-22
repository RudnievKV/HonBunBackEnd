using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.WordDtos
{
    public record WordCreateDto
    {
        public long Stage_ID { get; init; }
        public long WordCollection_ID { get; init; }
        public bool IsInSRS { get; init; }
        public IEnumerable<Definition> Definitions { get; init; }
    }
    public record Definition
    {
        public string PartOfSpeech { get; init; }
        public string Meaning { get; init; }
        public string Reading { get; init; }
        public string OriginalEntry { get; init; }
    }
}
