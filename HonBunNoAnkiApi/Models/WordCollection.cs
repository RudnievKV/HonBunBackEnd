using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Models
{
    public record WordCollection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long WordCollection_ID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset? UpdatedDate { get; init; }


        public virtual IEnumerable<Word>? Words { get; init; }
        public long User_ID { get; init; }
        public virtual User User { get; init; }
    }
}
