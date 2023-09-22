using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Models.DictionaryModels.NameModels
{
    public record Name
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        [BsonElement("ent_seq")]
        public string EntitySequence { get; init; }
        [BsonElement("k_ele")]
        public IEnumerable<NameKanjiElement> KanjiElement { get; init; }
        [BsonElement("r_ele")]
        public IEnumerable<NameReadingElement> ReadingElement { get; init; }
        [BsonElement("trans")]
        public IEnumerable<Translation> Translation { get; init; }
    }
}
