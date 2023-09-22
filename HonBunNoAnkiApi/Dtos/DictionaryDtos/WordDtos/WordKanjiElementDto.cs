using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
namespace HonbunNoAnkiApi.Dtos.DictionaryDtos.WordDtos
{
    public record WordKanjiElementDto
    {
        public IEnumerable<string> Kanjis { get; init; }
        public IEnumerable<string> KanjiInformations { get; init; }

    }
}
