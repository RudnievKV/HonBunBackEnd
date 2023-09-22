using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
namespace HonbunNoAnkiApi.Dtos.DictionaryDtos.WordDtos
{
    public record WordReadingElementDto
    {
        public IEnumerable<string> Readings { get; init; }
        public IEnumerable<string> ReadingInformations { get; init; }
    }
}
