using System.Collections.Generic;

namespace DictionaryApi.Dtos.NameDtos
{
    public record TranslationDto
    {
        public IEnumerable<string>? NameType { get; init; }
        public IEnumerable<string>? NameTranslation { get; init; }
    }
}
