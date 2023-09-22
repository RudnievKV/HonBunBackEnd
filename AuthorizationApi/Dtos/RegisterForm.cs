using System.ComponentModel.DataAnnotations;

namespace AuthorizationApi.Dtos
{
    public class RegisterForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }
        [Required]
        public string Username { get; init; }
    }
}
