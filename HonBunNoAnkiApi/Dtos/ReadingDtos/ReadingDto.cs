using HonbunNoAnkiApi.Dtos.WordDefinitionDtos;
using HonbunNoAnkiApi.Models;

namespace HonbunNoAnkiApi.Dtos.ReadingDtos
{
    public record ReadingDto
    {
        public long Reading_ID { get; init; }
        public string Value { get; init; }
    }
}
