using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace HonbunNoAnkiApi.Models
{
    public record MeaningReading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MeaningReading_ID { get; init; }
        public long? Word_ID { get; init; }
        public virtual Word Word { get; init; }
        public string OriginalEntry { get; init; }
        public string PartOfSpeech { get; init; }
        public string Meaning { get; init; }
        public string Reading { get; init; }

    }
}
