using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace HonbunNoAnkiApi.Models.DictionaryModels.KanjiModels
{
    public record Kanji
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        [BsonElement("literal")]
        public string Literal { get; init; }
        [BsonElement("codepoint")]
        public IEnumerable<CodePoint> CodePoints { get; init; }
        [BsonElement("radical")]
        public IEnumerable<Radical> Radicals { get; init; }
        [BsonElement("misc")]
        public Miscellanious Miscellanious { get; init; }
        [BsonElement("dic_number")]
        public IEnumerable<DictionaryNumber> DictionaryNumbers { get; init; }
        [BsonElement("query_code")]
        public IEnumerable<QueryCode> QueryCodes { get; init; }
        [BsonElement("reading_meaning")]
        public IEnumerable<ReadingMeaning> ReadingMeaning { get; init; }

    }
}
