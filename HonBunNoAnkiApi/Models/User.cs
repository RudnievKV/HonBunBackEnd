using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Models
{
    public record User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long User_ID { get; init; }
        public string Username { get; init; }
        public virtual IEnumerable<WordCollection> WordCollections { get; init; }
        public string Email { get; init; }
        public string PasswordHash { get; init; }
        public long CurrentExperience { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset? UpdatedDate { get; init; }
    }
}
