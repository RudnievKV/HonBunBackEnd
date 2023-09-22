using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace HonbunNoAnkiApi.Models.DictionaryModels.KanjiModels
{
    public record ReadingMeaning
    {
        [BsonElement("rmgroup")]
        public IEnumerable<ReadingMeaningGroup> ReadingMeaningGroups { get; init; }
        [BsonElement("nanori")]
        public IEnumerable<string> Nanori { get; init; }
    }
    public record ReadingMeaningGroup
    {
        [BsonElement("reading")]
        public IEnumerable<Reading> Readings { get; init; }
        [BsonElement("meaning")]
        public IEnumerable<string> Meanings { get; init; }
    }
    public record Reading
    {
        [BsonElement("@r_type")]
        public string ReadingType { get; init; }
        [BsonElement("#text")]
        public string Text { get; init; }
    }
}
