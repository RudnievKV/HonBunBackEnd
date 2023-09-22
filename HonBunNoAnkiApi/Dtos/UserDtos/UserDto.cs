using HonbunNoAnki.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
namespace HonbunNoAnkiApi.Dtos.UserDtos
{
    public record UserDto
    {
        public long User_ID { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public string PasswordHash { get; init; }
        public long CurrentExperience { get; init; }
        public long WordCollectionCount { get; set; }
        public long NumberOfWords { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset? UpdatedDate { get; init; }
    }
}
