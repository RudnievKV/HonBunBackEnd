using HonbunNoAnkiApi.Models;

namespace HonbunNoAnkiApi.Dtos.MeaningValueDtos
{
    public record MeaningValueDto
    {
        public long MeaningValue_ID { get; init; }
        public string Value { get; init; }
        public long Meaning_ID { get; init; }
    }
}
