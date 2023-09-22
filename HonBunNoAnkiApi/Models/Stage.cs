using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Models
{
    public record Stage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Stage_ID { get; init; }
        public virtual IEnumerable<Word> Words { get; init; }
        public int Duration { get; init; }
        public int StageNumber { get; init; }
        public string StageName { get; init; }
    }
}
