using HonbunNoAnkiApi.Common;
using HonbunNoAnkiApi.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Security.Claims;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Services;
using System.Threading.Tasks;
using HonbunNoAnkiApi.Dtos.WordDtos;
using Microsoft.AspNetCore.Authorization;
using HonbunNoAnkiApi.Dtos.DictionaryDtos.WordDtos;

namespace HonbunNoAnkiApi.Controllers
{
    [Route("api/words")]
    [ApiController]
    [Produces("application/json")]
    //[Authorize]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class WordController : ControllerBase
    {
        private long User_ID => long.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private readonly IWordService _wordService;
        public WordController(IWordService wordService)
        {
            _wordService = wordService;
        }

        [HttpGet]
        public async Task<ActionResult<WordDto>> GetWords()
        {
            try
            {
                var wordDtos = await _wordService.GetWords();


                return Ok(wordDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpGet("srs")]
        public async Task<ActionResult<WordDto>> GetSRSWords()
        {
            try
            {
                var wordDtos = await _wordService.GetSRSWords(User_ID);


                return Ok(wordDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpPut("srs/{id}")]
        public async Task<ActionResult<WordDto>> UpdateWordBasedOnReview(long id, [FromBody] WordUpdateReviewDto wordUpdateReviewDto)
        {
            try
            {
                var wordDto = await _wordService.UpdateWordBasedOnReview(id, wordUpdateReviewDto, User_ID);
                if (wordDto == null)
                {
                    return NotFound("Specified word asd.");
                }

                return Ok(wordDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<WordDto>> GetWord(long id)
        {
            try
            {
                var wordDto = await _wordService.GetWord(id);
                if (wordDto == null)
                {
                    return NotFound("Specified word does not exist.");
                }

                return Ok(wordDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpPost]
        public async Task<ActionResult<WordDto>> CreateWord([FromBody] WordCreateDto wordCreateDto)
        {
            try
            {
                var wordDto = await _wordService.CreateWord(wordCreateDto);

                return CreatedAtAction(nameof(CreateWord), wordDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<WordDto>> UpdateWord(long id,
            [FromBody] WordUpdateDto wordUpdateDto)
        {
            try
            {
                var word = await GetWord(id);
                if (word == null)
                {
                    return NotFound("Specified word does not exist.");
                }
                var wordDto = await _wordService.UpdateWord(id, wordUpdateDto);

                return Ok(wordDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWord(long id)
        {
            try
            {
                var result = await _wordService.DeleteWord(id);
                if (result == false)
                {
                    return NotFound("Specified word does not exist.");
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
