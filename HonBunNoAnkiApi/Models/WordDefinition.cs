using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Models
{
    public record WordDefinition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long WordDefinition_ID { get; init; }
        public long? Word_ID { get; init; }
        public virtual Word Word { get; init; }
        public string OriginalEntry { get; init; }
        public virtual IEnumerable<Meaning>? Meanings { get; init; }
        public virtual Reading? Reading { get; init; }

    }
}
