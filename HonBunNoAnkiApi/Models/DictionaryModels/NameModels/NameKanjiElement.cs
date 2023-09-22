using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace HonbunNoAnkiApi.Models.DictionaryModels.NameModels
{
    public record NameKanjiElement
    {
        [BsonElement("keb")]
        public string Kanji { get; init; }
        [BsonElement("ke_inf")]
        public string KanjiInformation { get; init; }
        [BsonElement("ke_pri")]
        public string KanjiPriority { get; init; }
    }
}
