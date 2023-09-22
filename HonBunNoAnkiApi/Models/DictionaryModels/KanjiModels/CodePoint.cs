using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace HonbunNoAnkiApi.Models.DictionaryModels.KanjiModels
{
    public record CodePoint
    {
        [BsonElement("cp_value")]
        public IEnumerable<CodePointValue> CodePointValue { get; init; }

    }
    public record CodePointValue
    {
        [BsonElement("@cp_type")]
        public string CodePointType { get; init; }
        [BsonElement("#text")]
        public string Text { get; init; }
    }
}
