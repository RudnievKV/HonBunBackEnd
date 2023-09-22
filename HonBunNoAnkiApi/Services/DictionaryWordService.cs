using AutoMapper;
using HonbunNoAnkiApi.Dtos.WordDtos;
using HonbunNoAnkiApi.Services.Interfaces;
using HonbunNoAnkiApi.Models.DictionaryModels;
using HonbunNoAnkiApi.Models.DictionaryModels.NameModels;
using HonbunNoAnkiApi.Models.DictionaryModels.WordModels;
using MeCab;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HonbunNoAnkiApi.Dtos.DictionaryDtos.NameDtos;
using HonbunNoAnkiApi.Dtos.DictionaryDtos.WordDtos;
using HonbunNoAnkiApi.DBThrottlePipeline;

namespace HonbunNoAnkiApi.Services
{
    public class DictionaryWordService : IDictionaryWordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConnectionThrottlingPipeline _connectionThrottlingPipeline;
        private readonly IMapper _mapper;
        public DictionaryWordService(IUnitOfWork unitOfWork, IConnectionThrottlingPipeline connectionThrottlingPipeline, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _connectionThrottlingPipeline = connectionThrottlingPipeline;
            _mapper = mapper;
        }
        private async Task<IEnumerable<IEnumerable<DictionaryWord>>> GetWords(IEnumerable<string> words)
        {
            var wordsList = new List<DictionaryWord>();
            var taskList = new List<Task<IAsyncCursor<DictionaryWord>>>();
            var funcList = new List<Func<Task<IAsyncCursor<DictionaryWord>>>>();


            foreach (var word in words)
            {
                Func<Task<IAsyncCursor<DictionaryWord>>> func = async () =>
                {
                    return await _unitOfWork.DictionaryWordRepo.GetWords("{ $or : [{k_ele : { $elemMatch : {  keb : " + $"'{word}'" + " }}}, {r_ele: { $elemMatch : { reb :  " + $"'{word}'" + "}}}]} ");
                };
                funcList.Add(func);
            }
            var cursorList = await _connectionThrottlingPipeline.AddRequestList(funcList);
            var resultList = new List<List<DictionaryWord>>();
            foreach (var cursor in cursorList)
            {
                resultList.Add(cursor.ToList());
            }
            return resultList;
            //var word = await (await _words.FindAsync(word => word.Id == "631f57407691873cc6cb93f6")).FirstOrDefaultAsync();

            //var count = await _words.CountDocumentsAsync(new BsonDocument());

        }
        private async Task<IEnumerable<IEnumerable<Name>>> GetNames(IEnumerable<string> names)
        {
            var namesList = new List<Name>();
            var taskList = new List<Task<IAsyncCursor<Name>>>();
            var funcList = new List<Func<Task<IAsyncCursor<Name>>>>();


            foreach (var name in names)
            {
                Func<Task<IAsyncCursor<Name>>> func = async () =>
                {
                    return await _unitOfWork.DictionaryWordRepo.GetNames("{ $or : [{k_ele :  { keb : " + $"'{name}'" + " }}, {r_ele : {reb : " + $"'{name}'" + " }}]}");
                };
                funcList.Add(func);
            }
            var cursorList = await _connectionThrottlingPipeline.AddRequestList(funcList);
            var resultList = new List<List<Name>>();
            foreach (var cursor in cursorList)
            {
                resultList.Add(cursor.ToList());
            }
            return resultList;
        }
        public async Task<IEnumerable<WordNameEntryDto>> GetWords(Request request)
        {
            var words = new SortedSet<string>();
            var names = new SortedSet<string>();
            var nameIdList = new List<int>();
            var text = request.Text;
            int count = 0;

            // PARSING

            var parameter = new MeCabParam();
            var tagger = MeCabTagger.Create(parameter);
            foreach (var node in tagger.ParseToNodes(text))
            {
                if (node.CharType > 0)
                {
                    var features = node.Feature.Split(',');
                    if (features[1] == "固有名詞") // check if the word is a proper noun
                    {
                        var displayFeatures = string.Join(", ", features);

                        if (names.Add(features[6])) // check if name already exists
                        {
                            nameIdList.Add(count);
                            //System.Diagnostics.Debug.WriteLine(features[6]);
                            System.Diagnostics.Debug.WriteLine($"{count}   {node.Surface}\t{displayFeatures}");
                            Interlocked.Increment(ref count);
                        }
                    }
                    else if (features[0] != "記号" && features[6] != "*")
                    {
                        var displayFeatures = string.Join(", ", features);

                        if (words.Add(features[6])) // check if the word already exists
                        {
                            //System.Diagnostics.Debug.WriteLine(features[6]);
                            System.Diagnostics.Debug.WriteLine($"{count}   {node.Surface}\t{displayFeatures}");
                            Interlocked.Increment(ref count);

                        }
                    }
                }
            }

            var dictionaryWordsTask = GetWords(words);
            var dictionaryNamesTask = GetNames(names);

            await Task.WhenAll(dictionaryWordsTask, dictionaryNamesTask);

            var dictionaryWords = dictionaryWordsTask.Result;
            var dictionaryNames = dictionaryNamesTask.Result;


            // MAPPING

            // MAPPING WORDS
            var wordDtos = new List<List<DictionaryWordDto>>();
            foreach (var wordList in dictionaryWords)
            {
                List<DictionaryWordDto> tempList = new List<DictionaryWordDto>();
                foreach (var word in wordList)
                {
                    var model = _mapper.Map<DictionaryWordDto>(word);
                    tempList.Add(model);

                }
                if (tempList.Count > 0)
                {
                    wordDtos.Add(tempList);
                }
                else
                {
                    wordDtos.Add(new List<DictionaryWordDto>());
                }
            }


            // MAPPING NAMES

            var namesDtos = new List<List<NameDto>>();
            foreach (var nameList in dictionaryNames)
            {
                var tempList = new List<NameDto>();
                foreach (var name in nameList)
                {
                    var model = _mapper.Map<NameDto>(name);
                    tempList.Add(model);

                }
                if (tempList.Count > 0)
                {
                    namesDtos.Add(tempList);
                }
                else
                {
                    namesDtos.Add(new List<NameDto>());
                }
            }





            // MAPPING RESULT

            var wordNameEntryDtos = new List<WordNameEntryDto>();
            var nameCount = 0;
            var wordCount = 0;
            for (int i = 0; i < words.Count + names.Count; i++)
            {
                if (nameIdList.Contains(i))
                {
                    // IT IS A NAME
                    if (namesDtos[nameCount].Count == 0)
                    {

                    }
                    else
                    {
                        var wordNameEntryDto = new WordNameEntryDto()
                        {
                            OriginalEntry = names.ElementAt(nameCount),
                            NameDtos = namesDtos[nameCount],
                            WordDtos = new List<DictionaryWordDto>(),
                        };
                        wordNameEntryDtos.Add(wordNameEntryDto);
                    }
                    nameCount++;
                }
                else
                {
                    // IT IS A WORD
                    if (wordDtos[wordCount].Count == 0)
                    {

                    }
                    else
                    {
                        var wordNameEntryDto = new WordNameEntryDto()
                        {
                            OriginalEntry = words.ElementAt(wordCount),
                            NameDtos = new List<NameDto>(),
                            WordDtos = wordDtos[wordCount],
                        };
                        wordNameEntryDtos.Add(wordNameEntryDto);
                    }
                    wordCount++;
                }
            }
            return wordNameEntryDtos;
            //foreach (var originalWord in words)
            //{
            //    if (nameIdList.Contains())
            //    var wordEntryDto = new WordNameEntryDto()
            //    {
            //        OriginalEntry = originalWord,
            //        WordDtos = wordDtos[count],
            //        NameDtos = namesDtos[count],
            //    };
            //    count++;
            //    wordEntryDtos.Add(wordEntryDto);
            //}



            //return (originalEntries: words, dictionaryEntries: dictionaryWords);
        }
    }
}
