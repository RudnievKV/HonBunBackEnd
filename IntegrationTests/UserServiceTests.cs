using AuthorizationApi.Dtos;
using AutoMapper;
using FakeItEasy;
using HonbunNoAnki;
using HonbunNoAnki.Models;
using HonbunNoAnkiApi.Controllers;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Services;
using HonbunNoAnkiApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using UserDto = HonbunNoAnkiApi.Dtos.UserDtos.UserDto;

namespace IntegrationTests
{
    public class UserServiceTests : BaseIntegrationTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task UserService_GetUsers_ReturnOK()
        {
            // Arrange
            await AuthenticateAsync();

            // Act

            var response = await _honbunClient.GetAsync(ApiRoutes.HonBunNoAnki.Users.GetAll);

            // Assert

            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}