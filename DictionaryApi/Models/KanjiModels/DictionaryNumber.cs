using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace DictionaryApi.Models.KanjiModels
{
    public record DictionaryNumber
    {
        [BsonElement("dic_ref")]
        public IEnumerable<DictionaryReference> DictionaryReferences { get; init; }
    }
    public record DictionaryReference
    {
        [BsonElement("@dr_type")]
        public string DictionaryType { get; init; }
        [BsonElement("@m_page")]
        public string Page { get; init; }
        [BsonElement("@m_vol")]
        public string Volume { get; init; }
        [BsonElement("#text")]
        public string Text { get; init; }

    }
}
