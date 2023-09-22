using AuthorizationApi.Models;
using AuthorizationApi.Repos.Interfaces;
using AuthorizationApi.Services.Interfaces;
using System.Security.Cryptography;
using System;
using System.Threading.Tasks;
using AuthorizationApi.Dtos;
using AutoMapper;
using AuthorizationApi.Common;

namespace AuthorizationApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;
        private readonly IMapper _mapper;
        public AuthService(IAuthRepo authRepo, IMapper mapper)
        {
            _authRepo = authRepo;
            _mapper = mapper;
        }
        public async Task<User> GetUser(string email, string password)
        {
            var user = await _authRepo.GetUser(email, password);
            if (user != null)
            {

                /* Fetch the stored value */
                string savedPasswordHash = user.PasswordHash;
                /* Extract the bytes */
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                /* Get the salt */
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                /* Compute the hash on the password the user entered */
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
                byte[] hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return null;
                    }
                }
                return user;
            }
            return null;
        }

        public async Task<UserDto> Register(RegisterForm registerForm)
        {
            var user = await GetUser(registerForm.Email, registerForm.Password);
            if (user is not null)
            {
                throw new Exception("User already exists");
            }

            PasswordHasher passwordHasher = new PasswordHasher();
            var passwordHash = passwordHasher.HashPassword(registerForm.Password);

            var newUser = new User()
            {
                Email = registerForm.Email,
                UserName = registerForm.Username,
                PasswordHash = passwordHash,
                CreatedDate = System.DateTimeOffset.Now
            };

            await _authRepo.Register(newUser);

            var addedUser = await GetUser(registerForm.Email, registerForm.Password);

            var userDto = _mapper.Map<UserDto>(addedUser);

            return userDto;
        }
    }
}
