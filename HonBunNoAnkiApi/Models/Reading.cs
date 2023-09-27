using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HonbunNoAnkiApi.Models
{
    public record Reading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Reading_ID { get; init; }
        public string Value { get; init; }
        public long WordDefinition_ID { get; init; }
        public virtual WordDefinition WordDefinition { get; init; }
    }
}
