using System.Collections.Generic;

namespace DictionaryApi.Dtos.WordDtos
{
    public record SenseDto
    {
        public IEnumerable<string>? PartOfSpeeches { get; init; }
        public IEnumerable<string>? Dialects { get; init; }
        public IEnumerable<string>? Informations { get; init; }
        public IEnumerable<string>? Glosses { get; init; }
        public IEnumerable<string>? Miscellaneous { get; init; }


    }
}
