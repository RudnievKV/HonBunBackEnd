using HonbunNoAnkiApi.Dtos.WordDtos;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.WordCollectionDtos
{
    public record WordCollectionUpdateDto
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
