﻿namespace HonbunNoAnkiApi.Dtos.UserDtos
{
    public record UserUpdateDto
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
