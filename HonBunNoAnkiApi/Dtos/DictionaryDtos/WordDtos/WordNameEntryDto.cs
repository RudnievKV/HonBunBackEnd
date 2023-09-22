using HonbunNoAnkiApi.Dtos.DictionaryDtos.NameDtos;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.DictionaryDtos.WordDtos
{
    public record WordNameEntryDto
    {
        public string OriginalEntry { get; init; }
        public IEnumerable<DictionaryWordDto> WordDtos { get; init; }
        public IEnumerable<NameDto> NameDtos { get; init; }
    }
}
