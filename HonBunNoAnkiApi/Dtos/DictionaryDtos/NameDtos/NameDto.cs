﻿using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.DictionaryDtos.NameDtos
{
    public record NameDto
    {
        public string Id { get; init; }
        public IEnumerable<NameKanjiElementDto> KanjiElement { get; init; }
        public IEnumerable<NameReadingElementDto> ReadingElement { get; init; }
        public IEnumerable<TranslationDto> Translation { get; init; }
    }
}
