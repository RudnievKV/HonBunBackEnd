using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.DictionaryDtos.NameDtos
{
    public record NameReadingElementDto
    {
        public string Reading { get; init; }
        public string ReadingInformation { get; init; }
    }
}
