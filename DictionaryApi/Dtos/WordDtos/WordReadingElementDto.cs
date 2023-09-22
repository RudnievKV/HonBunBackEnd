using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
namespace DictionaryApi.Dtos.WordDtos
{
    public record WordReadingElementDto
    {
        public IEnumerable<string> Readings { get; init; }
        public IEnumerable<string>? ReadingInformations { get; init; }
    }
}
