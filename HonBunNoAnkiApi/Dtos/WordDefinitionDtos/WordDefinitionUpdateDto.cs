using HonbunNoAnkiApi.Dtos.MeaningDtos;
using HonbunNoAnkiApi.Dtos.ReadingDtos;

namespace HonbunNoAnkiApi.Dtos.WordDefinitionDtos
{
    public record WordDefinitionUpdateDto
    {
        public long? Word_ID { get; init; }
        public string OriginalEntry { get; init; }
        public IEnumerable<MeaningUpdateDto> Meanings { get; init; }
        public ReadingUpdateDto Reading { get; init; }
    }
}
