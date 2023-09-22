using FakeItEasy;
using FluentAssertions;
using HonbunNoAnkiApi.Controllers;
using HonbunNoAnkiApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests
{
    public class UserControllerTests
    {
        private IUserService _userService;
        [SetUp]
        public void Setup()
        {
            _userService = A.Fake<IUserService>();
        }

        [Test]
        public async Task UserController_GetUsers_ReturnOK()
        {
            // Arrange

            var userController = new UserController(_userService);


            // Act

            var actionResult = await userController.GetUsers();
            var result = actionResult.Result;

            // Assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}