using HonbunNoAnkiApi.Dtos.DictionaryDtos.WordDtos;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Dtos.WordDtos;
using System;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.WordCollectionDtos
{
    public record WordCollectionDto
    {
        public long WordCollection_ID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public virtual IEnumerable<WordDto>? Words { get; init; }
        public virtual UserDto User { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset? UpdatedDate { get; init; }
    }
}
