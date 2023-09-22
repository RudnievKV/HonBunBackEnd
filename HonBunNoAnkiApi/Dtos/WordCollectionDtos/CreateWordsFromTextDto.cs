namespace HonbunNoAnkiApi.Dtos.WordCollectionDtos
{
    public record CreateWordsFromTextDto
    {
        public string Text { get; init; }
        public long WordCollectionID { get; init; }
    }
}
