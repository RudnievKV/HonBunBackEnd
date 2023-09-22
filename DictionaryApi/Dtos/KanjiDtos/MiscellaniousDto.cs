using DictionaryApi.Models.KanjiModels;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DictionaryApi.Dtos.KanjiDtos
{
    public record MiscellaniousDto
    {
        public string Grade { get; init; }
        public string Frequency { get; init; }
        public string JLPT { get; init; }
    }
}
