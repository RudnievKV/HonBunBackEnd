using HonbunNoAnkiApi.Dtos.MeaningDtos;
using HonbunNoAnkiApi.Dtos.ReadingDtos;

namespace HonbunNoAnkiApi.Dtos.WordDefinitionDtos
{
    public record WordDefinitionCreateDto
    {
        public long? Word_ID { get; init; }
        public string OriginalEntry { get; init; }
        public IEnumerable<MeaningCreateDto> Meanings { get; init; }
        public ReadingCreateDto Reading { get; init; }
    }
}
