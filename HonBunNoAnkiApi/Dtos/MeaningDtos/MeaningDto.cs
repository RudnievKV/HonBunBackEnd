namespace HonbunNoAnkiApi.Dtos.MeaningDtos
{
    public record MeaningDto
    {
        public long Meaning_ID { get; init; }
        public IEnumerable<string> Meanings { get; init; }
        public string PartOfSpeech { get; init; }
    }
}
