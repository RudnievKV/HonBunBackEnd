using HonbunNoAnkiApi.Services;
using HonbunNoAnkiApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using HonbunNoAnkiApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using HonbunNoAnkiApi.Common;
using HonbunNoAnkiApi.Models.DictionaryModels;

namespace HonbunNoAnkiApi.Controllers
{
    [Route("api/dictionary/kanji")]
    [ApiController]
    [Produces("application/json")]
    [AllowAnonymous]
    [EnableCors(CORSPolicies.StandartCORSPolicy)]
    public class KanjiController : ControllerBase
    {
        private readonly IDictionaryKanjiService _kanjiService;

        public KanjiController(IDictionaryKanjiService kanjiService)
        {
            this._kanjiService = kanjiService;
        }

        // GET: api/word
        [HttpPost]
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
