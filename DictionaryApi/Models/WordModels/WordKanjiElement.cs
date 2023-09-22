using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DictionaryApi.Models.WordModels
{
    public record WordKanjiElement
    {
        [BsonElement("keb")]
        public IEnumerable<string> Kanjis { get; init; }
        [BsonElement("ke_inf")]
        public IEnumerable<string>? KanjiInformations { get; init; }
        [BsonElement("ke_pri")]
        public IEnumerable<string>? KanjiPriorities { get; init; }
    }
}
