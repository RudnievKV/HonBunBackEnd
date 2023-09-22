using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.DictionaryDtos.NameDtos
{
    public record TranslationDto
    {
        public IEnumerable<string> NameType { get; init; }
        public IEnumerable<string> NameTranslation { get; init; }
    }
}
