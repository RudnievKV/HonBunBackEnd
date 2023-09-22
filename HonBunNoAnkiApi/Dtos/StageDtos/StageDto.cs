using System.Collections.Generic;

namespace HonbunNoAnkiApi.Dtos.StageDtos
{
    public record StageDto
    {
        public long Stage_ID { get; init; }
        public int Duration { get; init; }
        public int StageNumber { get; init; }
        public string StageName { get; init; }
    }
}
