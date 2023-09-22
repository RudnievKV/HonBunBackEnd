using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AuthorizationApi.Models
{
    public record User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long User_ID { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string PasswordHash { get; init; }
        public long CurrentExperience { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset? UpdatedDate { get; init; }
    }
}
