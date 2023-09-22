using HonbunNoAnkiApi.Dtos.WordDtos;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.WordCollectionDtos
{
    public record WordCollectionCreateDto
    {
        public long User_ID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        //public virtual IEnumerable<WordCreate>? Words { get; init; }
    }
    public record WordCreate
    {
        public long Stage_ID { get; init; }
        public bool IsInSRS { get; init; }
        public IEnumerable<Definition> Definitions { get; init; }
    }
}
