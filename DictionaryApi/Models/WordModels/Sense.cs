using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DictionaryApi.Models.WordModels
{
    public record Sense
    {
        [BsonElement("stagk")]
        public IEnumerable<string>? stagk { get; init; }
        [BsonElement("stagr")]
        public IEnumerable<string>? stagr { get; init; }
        [BsonElement("pos")]
        public IEnumerable<string>? PartOfSpeeches { get; init; }
        [BsonElement("xref")]
        public IEnumerable<string>? CrossReferences { get; init; }
        [BsonElement("ant")]
        public IEnumerable<string>? Antonyms { get; init; }
        [BsonElement("field")]
        public IEnumerable<string>? FieldOfApplications { get; init; }
        [BsonElement("misc")]
        public IEnumerable<string>? Miscellaneous { get; init; }
        [BsonElement("lsource")]
        public IEnumerable<string>? LanguageSources { get; init; }
        [BsonElement("s_inf")]
        public IEnumerable<string>? Informations { get; init; }
        [BsonElement("dial")]
        public IEnumerable<string>? Dialects { get; init; }
        [BsonElement("gloss")]
        public IEnumerable<string>? Glosses { get; init; }
        [BsonElement("example")]
        public IEnumerable<string>? Examples { get; init; }
    }
}
