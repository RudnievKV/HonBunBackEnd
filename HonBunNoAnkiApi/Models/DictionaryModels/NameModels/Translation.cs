using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace HonbunNoAnkiApi.Models.DictionaryModels.NameModels
{
    public record Translation
    {
        [BsonElement("name_type")]
        public IEnumerable<string> NameType { get; init; }
        [BsonElement("xref")]
        public string CrossReference { get; init; }
        [BsonElement("trans_det")]
        public IEnumerable<string> NameTranslation { get; init; }
    }
}
