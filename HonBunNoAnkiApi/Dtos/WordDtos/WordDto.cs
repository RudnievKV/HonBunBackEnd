using HonbunNoAnkiApi.Models;
using System.Collections.Generic;
using System;
using HonbunNoAnkiApi.Dtos.StageDtos;
using HonbunNoAnkiApi.Dtos.MeaningReadingDtos;

namespace HonbunNoAnkiApi.Dtos.WordDtos
{
    public record WordDto
    {
        public long Word_ID { get; init; }
        public virtual StageDto Stage { get; init; }

        public long WordCollection_ID { get; init; }
        //public virtual WordCollectionDto WordCollection { get; init; }
        public virtual IEnumerable<MeaningReadingDto>? MeaningReadings { get; init; }
        public bool IsInSRS { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset? UpdatedDate { get; init; }
        public DateTimeOffset? StartInitialSRSDate { get; init; }
        public DateTimeOffset? StartCurrentSRSDate { get; init; }
    }
}
