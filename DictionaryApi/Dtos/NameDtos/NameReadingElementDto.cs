using System.Collections.Generic;

namespace DictionaryApi.Dtos.NameDtos
{
    public record NameReadingElementDto
    {
        public string Reading { get; init; }
        public string ReadingInformation { get; init; }
    }
}
