namespace HonbunNoAnkiApi.Dtos.MeaningDtos
{
    public record MeaningUpdateDto
    {
        public string PartOfSpeech { get; init; }
        public IEnumerable<string> Meanings { get; init; }
    }
}
