using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HonbunNoAnkiApi.Models
{
    public record MeaningValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MeaningValue_ID { get; init; }
        public string Value { get; init; }
        public Meaning Meaning { get; init; }
        public long Meaning_ID { get; init; }
    }
}
