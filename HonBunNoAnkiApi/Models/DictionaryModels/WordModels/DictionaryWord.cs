using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Models.DictionaryModels.WordModels
{
    //[BsonIgnoreExtraElements]
    public record DictionaryWord
    {

        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        [BsonElement("ent_seq")]
        public IEnumerable<string> EntitySequence { get; init; }
        [BsonElement("k_ele")]
        public IEnumerable<WordKanjiElement> KanjiElements { get; init; }
        [BsonElement("r_ele")]
        public IEnumerable<WordReadingElement> ReadingElements { get; init; }
        [BsonElement("sense")]
        public IEnumerable<Sense> Senses { get; init; }
    }
}
