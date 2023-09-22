using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DictionaryApi.Dtos.KanjiDtos
{
    public record ReadingDto
    {
        public string ReadingType { get; init; }
        public string Text { get; init; }
    }
}
