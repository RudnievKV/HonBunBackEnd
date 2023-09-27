using HonbunNoAnkiApi.Dtos.MeaningDtos;
using HonbunNoAnkiApi.Dtos.ReadingDtos;
using HonbunNoAnkiApi.Dtos.WordDtos;
using HonbunNoAnkiApi.Models;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.WordDefinitionDtos
{
    public record WordDefinitionDto
    {
        public long WordDefinition_ID { get; init; }
        public long? Word_ID { get; init; }
        public string OriginalEntry { get; init; }
        public virtual IEnumerable<MeaningDto>? Meanings { get; init; }
        public virtual ReadingDto? Reading { get; init; }
    }
}
