using HonbunNoAnkiApi.Common;
using HonbunNoAnkiApi.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Security.Claims;
using HonbunNoAnkiApi.Dtos.UserDtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace HonbunNoAnkiApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Produces("application/json")]
    //[Authorize]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class UserController : ControllerBase
    {
        private long User_ID => long.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private readonly IUserService _userService;
        public UserController(IUserService userTypeService)
        {
            _userService = userTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUsers()
        {
            try
            {
                var userDtos = await _userService.GetUsers();


                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(long id)
        {
            try
            {
                var userDto = await _userService.GetUser(id);
                if (userDto == null)
                {
                    return NotFound("Specified user does not exist.");
                }

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            try
            {
                var userDto = await _userService.CreateUser(userCreateDto);
                if (userDto == null)
                {
                    return BadRequest("Email is not unique.");
                }

                return CreatedAtAction(nameof(CreateUser), userDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(long id,
            [FromBody] UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await GetUser(id);
                if (user == null)
                {
                    return NotFound("Specified user does not exist.");
                }
                var userDto = await _userService.UpdateUser(id, userUpdateDto);
                if (userDto == null)
                {
                    return BadRequest("Email is not unique.");
                }

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                await _userService.DeleteUser(id);

                return Ok();
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
    }
}
