using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.WordDtos
{
    public record WordUpdateDto
    {
        public long Stage_ID { get; init; }
        public long WordCollection_ID { get; init; }
        public bool IsInSRS { get; init; }
        public IEnumerable<Definition> Definitions { get; init; }
    }
}
