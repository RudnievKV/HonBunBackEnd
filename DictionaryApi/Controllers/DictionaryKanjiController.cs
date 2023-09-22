using DictionaryApi.Services;
using DictionaryApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using DictionaryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using DictionaryApi.Common;

namespace DictionaryApi.Controllers
{
    [Route("api/kanji")]
    [ApiController]
    [Produces("application/json")]
    [AllowAnonymous]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class DictionaryKanjiController : ControllerBase
    {
        private readonly IDictionaryKanjiService _kanjiService;

        public DictionaryKanjiController(IDictionaryKanjiService kanjiService)
        {
            this._kanjiService = kanjiService;
        }

        [HttpPost]
        public IActionResult PostKanji()
        {
            try
            {
                return Ok("OK.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // GET: api/word
        [HttpGet]
        public async Task<IActionResult> GetKanji([FromBody] Request request)
        {
            try
            {
                var kanjiEntryDtos = await _kanjiService.GetKanjis(request);


                return Ok(kanjiEntryDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




    }
}
