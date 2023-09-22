using AuthorizationApi;
using AuthorizationApi.Dtos;
using HonbunNoAnkiApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace IntegrationTests
{
    public class BaseIntegrationTest
    {
        protected readonly HttpClient _authClient;
        protected readonly HttpClient _honbunClient;

        protected BaseIntegrationTest()
        {
            var authAppFactory = new WebApplicationFactory<StartupAuth>();
            _authClient = authAppFactory.CreateClient();
            _authClient.BaseAddress = new Uri(ApiRoutes.Authorization.Root);

            var honbunAppFactory = new WebApplicationFactory<StartupHonbun>();
            _honbunClient = honbunAppFactory.CreateClient();
            _honbunClient.BaseAddress = new Uri(ApiRoutes.HonBunNoAnki.Root);
        }

        protected async Task AuthenticateAsync()
        {
            _honbunClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {

            var response = await _authClient.PostAsJsonAsync(ApiRoutes.Authorization.Login, new LoginForm
            {
                Email = "user@example.com",
                Password = "string"
            });
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception(HttpStatusCode.Unauthorized.ToString());
            }
        }
    }
}
