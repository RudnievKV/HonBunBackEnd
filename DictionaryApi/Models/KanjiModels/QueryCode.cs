using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace DictionaryApi.Models.KanjiModels
{
    public record QueryCode
    {
        [BsonElement("q_code")]
        public IEnumerable<QCode> QueryCodes { get; init; }
    }
    public record QCode
    {
        [BsonElement("@qc_type")]
        public string QueryCodeType { get; init; }
        [BsonElement("#text")]
        public string Text { get; init; }
        [BsonElement("@skip_misclass")]
        public string Missclassification { get; init; }
    }
}
