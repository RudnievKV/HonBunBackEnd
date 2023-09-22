using AuthorizationApi.Common;
using AuthorizationApi.Dtos;
using AuthorizationApi.Repos.Interfaces;
using AuthorizationApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Produces("application/json")]
    [AllowAnonymous]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IOptions<AuthOptions> authOptions;
        public AuthController(IAuthService authService, IOptions<AuthOptions> authOptions)
        {
            this._authService = authService;
            this.authOptions = authOptions;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginForm request)
        {
            try
            {
                var account = await AuthenticateUser(request.Email, request.Password);

                if (account != null)
                {
                    var token = GenerateJWT(account);

                    return Ok(new
                    {
                        access_token = token,
                        user_ID = account.ID,
                    });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [Route("check")]
        [HttpPost]
        public async Task<IActionResult> CheckUser(LoginForm request)
        {
            try
            {
                var user = await _authService.GetUser(request.Email, request.Password);


                if (user != null)
                {
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterForm registerForm)
        {
            try
            {
                var newUser = await _authService.Register(registerForm);

                return CreatedAtAction(nameof(Register), newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        private async Task<Account> AuthenticateUser(string email, string password)
        {
            var user = await _authService.GetUser(email, password);
            if (user != null)
            {

                var account = new Account()
                {
                    Email = email,
                    Password = password,
                    ID = user.User_ID
                };
                return account;
            }
            return null;
        }

        private string GenerateJWT(Account user)
        {
            var authParams = authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString())
            };


            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
