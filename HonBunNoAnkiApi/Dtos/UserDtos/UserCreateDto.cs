using System;

namespace HonbunNoAnkiApi.Dtos.UserDtos
{
    public record UserCreateDto
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
