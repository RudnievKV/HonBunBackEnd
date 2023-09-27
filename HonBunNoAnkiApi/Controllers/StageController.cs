using HonbunNoAnkiApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Cors;
using HonbunNoAnkiApi.Common;
using System.Security.Claims;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Services;
using System.Threading.Tasks;
using HonbunNoAnkiApi.Dtos.StageDtos;
using Microsoft.AspNetCore.Authorization;

namespace HonbunNoAnkiApi.Controllers
{
    [Route("api/stages")]
    [ApiController]
    [Produces("application/json")]
    //[Authorize]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class StageController : ControllerBase
    {
        private long User_ID => long.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private readonly IStageService _stageService;
        public StageController(IStageService stageService)
        {
            _stageService = stageService;
        }
        [HttpGet]
        public async Task<ActionResult<StageDto>> GetStages()
        {
            try
            {
                var stageDtos = await _stageService.GetStages();


                return Ok(stageDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StageDto>> GetStage(long id)
        {
            try
            {
                var stageDto = await _stageService.GetStage(id);
                if (stageDto == null)
                {
                    return NotFound("Specified stage does not exist.");
                }

                return Ok(stageDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpPost]
        public async Task<ActionResult<StageDto>> CreateStage([FromBody] StageCreateDto stageCreateDto)
        {
            try
            {
                var stageDto = await _stageService.CreateStage(stageCreateDto);


                return CreatedAtAction(nameof(CreateStage), stageDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<StageDto>> UpdateStage(long id,
            [FromBody] StageUpdateDto stageUpdateDto)
        {
            try
            {
                var stageDto = await _stageService.UpdateStage(id, stageUpdateDto);
                if (stageDto == null)
                {
                    return BadRequest("Specified stage does not exist.");
                }

                return Ok(stageDto);
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
                var result = await _stageService.DeleteStage(id);
                if (result == false)
                {
                    return NotFound("Specified stage does not exist.");
                }

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
