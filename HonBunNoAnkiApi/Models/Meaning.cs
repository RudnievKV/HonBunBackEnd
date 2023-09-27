using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HonbunNoAnkiApi.Models
{
    public record Meaning
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Meaning_ID { get; init; }
        public IEnumerable<MeaningValue>? MeaningValues { get; init; }
        public string? PartOfSpeech { get; init; }
        public long WordDefinition_ID { get; init; }
        public virtual WordDefinition WordDefinition { get; init; }
    }
}
