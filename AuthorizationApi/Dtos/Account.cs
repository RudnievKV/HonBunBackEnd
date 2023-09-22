using System;

namespace AuthorizationApi.Dtos
{
    public record Account
    {
        public long ID { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
