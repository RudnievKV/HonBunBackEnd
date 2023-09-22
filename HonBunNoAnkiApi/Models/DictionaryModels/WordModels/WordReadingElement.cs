using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Models.DictionaryModels.WordModels
{
    public record WordReadingElement
    {
        [BsonElement("reb")]
        public IEnumerable<string> Readings { get; init; }
        [BsonElement("re_nokanji")]
        public IEnumerable<string> NoKanji { get; init; }
        [BsonElement("re_restr")]
        public IEnumerable<string> Restrictions { get; init; }
        [BsonElement("re_inf")]
        public IEnumerable<string> ReadingInformations { get; init; }
        [BsonElement("re_pri")]
        public IEnumerable<string> ReadingPriorities { get; init; }

    }
}
