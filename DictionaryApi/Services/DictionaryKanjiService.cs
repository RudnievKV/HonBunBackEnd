using AutoMapper;
using DictionaryApi.DBContext;
using DictionaryApi.Dtos.KanjiDtos;
using DictionaryApi.Dtos.NameDtos;
using DictionaryApi.Models;
using DictionaryApi.Models.KanjiModels;
using DictionaryApi.Models.NameModels;
using DictionaryApi.Services.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DictionaryApi.Services
{
    public class DictionaryKanjiService : IDictionaryKanjiService
    {
        private readonly IMongoCollection<Kanji> _kanji;
        private readonly IConnectionThrottlingPipeline _connectionThrottlingPipeline;
        private readonly IMapper _mapper;
        public DictionaryKanjiService(IDBSettings settings, MongoClient mongoClient, IConnectionThrottlingPipeline connectionThrottlingPipeline, IMapper mapper)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            this._kanji = database.GetCollection<Kanji>(settings.KanjiCollectionName);
            this._connectionThrottlingPipeline = connectionThrottlingPipeline;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KanjiEntryDto>> GetKanjis(Request request)
        {
            string[] kanjis = request.Text.Select(x => new string(x, 1)).ToArray();

            var kanjiList = new List<Kanji>();
            var taskList = new List<Task<IAsyncCursor<Kanji>>>();
            var funcList = new List<Func<Task<IAsyncCursor<Kanji>>>>();
            var options = new FindOptions<Kanji>
            {
                BatchSize = 1
            };

            foreach (var kanji in kanjis)
            {
                Func<Task<IAsyncCursor<Kanji>>> func = async () =>
                {
                    return await _kanji.FindAsync("{ literal : " + $"'{kanji}'" + "}", options);
                };
                funcList.Add(func);
            }
            var cursorList = await _connectionThrottlingPipeline.AddRequestList(funcList);
            var resultList = new List<Kanji>();
            foreach (var cursor in cursorList)
            {
                resultList.Add(cursor.FirstOrDefault());
            }

            var result = resultList;


            var kanjiDtos = new List<KanjiEntryDto>();
            var count = 0;
            foreach (var kanji in result)
            {
                var kanjiDto = _mapper.Map<KanjiDto>(kanji);
                //kanjiDtos.Add(model);
                if (kanjiDto == null)
                {
                    var kanjiEntryDto = new KanjiEntryDto()
                    {
                        OriginalEntry = kanjis[count],
                        KanjiDto = new KanjiDto(),
                    };
                    kanjiDtos.Add(kanjiEntryDto);
                }
                else
                {
                    var kanjiEntryDto = new KanjiEntryDto()
                    {
                        OriginalEntry = kanjis[count],
                        KanjiDto = kanjiDto,
                    };
                    kanjiDtos.Add(kanjiEntryDto);
                }

                count++;
            }
            return kanjiDtos;
        }
    }
}
