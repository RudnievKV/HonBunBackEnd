using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace DictionaryApi.Models.KanjiModels
{
    public record Miscellanious
    {
        [BsonElement("grade")]
        public string Grade { get; init; } = "";
        [BsonElement("stroke_count")]
        public IEnumerable<string> StrokeCount { get; init; }
        [BsonElement("variant")]
        public IEnumerable<Variant> Variants { get; init; }
        [BsonElement("freq")]
        public string Frequency { get; init; } = "";
        [BsonElement("rad_name")]
        public IEnumerable<string> RadicalName { get; init; }
        [BsonElement("jlpt")]
        public string JLPT { get; init; } = "";
    }
    public record Variant
    {
        [BsonElement("@var_type")]
        public string VariantType { get; init; }
        [BsonElement("#text")]
        public string Text { get; init; }
    }
}
