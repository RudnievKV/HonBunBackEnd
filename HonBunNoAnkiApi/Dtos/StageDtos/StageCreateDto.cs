using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.StageDtos
{
    public record StageCreateDto
    {
        public int Duration { get; init; }
        public int StageNumber { get; init; }
        public string StageName { get; init; }
    }
}
