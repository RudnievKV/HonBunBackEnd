namespace HonbunNoAnkiApi.Dtos.MeaningDtos
{
    public record MeaningCreateDto
    {
        public IEnumerable<string> Meanings { get; init; }
        public string PartOfSpeech { get; init; }
    }
}
