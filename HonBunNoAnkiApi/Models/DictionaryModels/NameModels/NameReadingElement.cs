using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace HonbunNoAnkiApi.Models.DictionaryModels.NameModels
{
    public record NameReadingElement
    {
        [BsonElement("reb")]
        public string Reading { get; init; }
        [BsonElement("re_restr")]
        public string Restriction { get; init; }
        [BsonElement("re_inf")]
        public string ReadingInformation { get; init; }
        [BsonElement("re_pri")]
        public string ReadingPriority { get; init; }
    }
}
