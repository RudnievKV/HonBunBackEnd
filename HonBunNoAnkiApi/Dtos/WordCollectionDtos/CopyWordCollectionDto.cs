namespace HonbunNoAnkiApi.Dtos.WordCollectionDtos
{
    public record CopyWordCollectionDto
    {
        public long WordCollection_ID { get; init; }
        public long User_ID { get; init; }
    }
}
