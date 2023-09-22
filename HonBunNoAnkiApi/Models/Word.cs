using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Models
{
    public record Word
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Word_ID { get; init; }
        public long WordCollection_ID { get; init; }
        public long? Stage_ID { get; init; }
        public virtual Stage Stage { get; init; }
        public virtual WordCollection WordCollection { get; init; }
        public virtual IEnumerable<MeaningReading> MeaningReadings { get; init; }
        public bool IsInSRS { get; init; }
        public DateTimeOffset? StartInitialSRSDate { get; init; }
        public DateTimeOffset? StartCurrentSRSDate { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset? UpdatedDate { get; init; }

    }
}
