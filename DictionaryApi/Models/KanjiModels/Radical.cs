using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace DictionaryApi.Models.KanjiModels
{
    public record Radical
    {
        [BsonElement("rad_value")]
        public IEnumerable<RadicalValue> Radicals { get; init; }
    }
    public record RadicalValue
    {
        [BsonElement("@rad_type")]
        public string RadicalType { get; init; }
        [BsonElement("#text")]
        public string Text { get; init; }
    }
}
