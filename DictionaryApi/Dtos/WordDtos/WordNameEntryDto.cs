using DictionaryApi.Dtos.NameDtos;
using System.Collections.Generic;

namespace DictionaryApi.Dtos.WordDtos
{
    public record WordNameEntryDto
    {
        public string OriginalEntry { get; init; }
        public IEnumerable<WordDto>? WordDtos { get; init; }
        public IEnumerable<NameDto>? NameDtos { get; init; }
    }
}
