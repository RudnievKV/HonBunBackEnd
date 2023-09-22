using HonbunNoAnkiApi.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using HonbunNoAnkiApi.Services.Interfaces;
using System.Security.Claims;
using HonbunNoAnkiApi.Dtos.WordDtos;
using System.Threading.Tasks;
using HonbunNoAnkiApi.Dtos.WordCollectionDtos;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HonbunNoAnkiApi.Controllers
{
    [Route("api/wordCollections")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class WordCollectionController : ControllerBase
    {
        private long User_ID => long.Parse(User.Claims.Single(s => s.Type == ClaimTypes.NameIdentifier).Value);
        private readonly IWordCollectionService _wordCollectionService;
        public WordCollectionController(IWordCollectionService wordCollectionService)
        {
            _wordCollectionService = wordCollectionService;
        }
        [HttpGet]
        public async Task<ActionResult<WordCollectionDto>> GetWordCollections()
        {
            try
            {
                var wordCollectionDtos = await _wordCollectionService.GetWordCollections(User_ID);


                return Ok(wordCollectionDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpGet("user")]
        public async Task<ActionResult<WordCollectionDto>> GetUserWordCollections()
        {
            try
            {
                var wordUserCollectionDtos = await _wordCollectionService.GetUserWordCollections(User_ID);


                return Ok(wordUserCollectionDtos);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<WordCollectionDto>> GetWordCollection(long id)
        {
            try
            {
                var wordCollectionDto = await _wordCollectionService.GetWordCollection(id);
                if (wordCollectionDto == null)
                {
                    return NotFound("Specified wordCollection does not exist.");
                }

                return Ok(wordCollectionDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpPost]
        public async Task<ActionResult<WordCollectionDto>> CreateWordCollection([FromBody] WordCollectionCreateDto wordCollectionCreateDto)
        {
            try
            {
                var wordCollectionDto = await _wordCollectionService.CreateWordCollection(wordCollectionCreateDto);

                return CreatedAtAction(nameof(CreateWordCollection), wordCollectionDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpPost("copyWordCollection")]
        public async Task<ActionResult<WordCollectionDto>> CopyWordCollection([FromBody] CopyWordCollectionDto copyWordCollectionDto)
        {
            try
            {
                var wordCollectionDto = await _wordCollectionService.CopyWordCollection(copyWordCollectionDto.WordCollection_ID, copyWordCollectionDto.User_ID);

                return CreatedAtAction(nameof(CopyWordCollection), wordCollectionDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }

        [HttpPost("createWordsFromText")]
        public async Task<ActionResult<WordCollectionDto>> CreateWordsFromText([FromBody] CreateWordsFromTextDto createWordsFromTextDto)
        {
            try
            {
                var wordCollectionDto = await _wordCollectionService.CreateWordsFromText(createWordsFromTextDto.Text, createWordsFromTextDto.WordCollectionID);

                return CreatedAtAction(nameof(CreateWordsFromText), wordCollectionDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<WordCollectionDto>> UpdateWordCollection(long id,
            [FromBody] WordCollectionUpdateDto wordCollectionUpdateDto)
        {
            try
            {
                var wordCollection = await GetWordCollection(id);
                if (wordCollection == null)
                {
                    return NotFound("Specified wordCollection does not exist.");
                }
                var wordCollectionDto = await _wordCollectionService.UpdateWordCollection(id, wordCollectionUpdateDto);

                return Ok(wordCollectionDto);
            }
            catch (Exception ex)
            {
                var errors = ExceptionHandler.PackErrorsToList(ex);
                return StatusCode(500, errors);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWordCollection(long id)
        {
            try
            {
                var result = await _wordCollectionService.DeleteWordCollection(id);
                if (result == false)
                {
                    return NotFound("Specified wordCollection does not exist.");
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
