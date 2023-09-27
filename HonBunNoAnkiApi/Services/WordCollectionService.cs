using AutoMapper;
using HonbunNoAnkiApi.DBThrottlePipeline;
using HonbunNoAnkiApi.Dtos.DictionaryDtos.NameDtos;
using HonbunNoAnkiApi.Dtos.DictionaryDtos.WordDtos;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Dtos.WordCollectionDtos;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Models.DictionaryModels;
using HonbunNoAnkiApi.Models.DictionaryModels.NameModels;
using HonbunNoAnkiApi.Models.DictionaryModels.WordModels;
using HonbunNoAnkiApi.Services.Interfaces;
using MeCab;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace HonbunNoAnkiApi.Services
{
    public class WordCollectionService : IWordCollectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConnectionThrottlingPipeline _connectionThrottlingPipeline;
        private readonly IMapper _mapper;
        public WordCollectionService(IUnitOfWork unitOfWork, IConnectionThrottlingPipeline connectionThrottlingPipeline, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _connectionThrottlingPipeline = connectionThrottlingPipeline;
            _mapper = mapper;
        }
        public async Task<WordCollectionDto> GetWordCollection(long id)
        {
            var wordCollection = await _unitOfWork.WordCollectionRepo.GetWordCollection(id);

            var wordCollectionDto = _mapper.Map<WordCollectionDto>(wordCollection);
            return wordCollectionDto;
        }

        public async Task<IEnumerable<WordCollectionDto>> GetUserWordCollections(long id)
        {
            var wordCollections = await _unitOfWork.WordCollectionRepo
                .Find(s => s.User_ID == id)
                .Include(s => s.User)
                .Include(s => s.Words).ThenInclude(s => s.WordDefinitions)
                .Include(s => s.Words).ThenInclude(s => s.Stage)
                .ToListAsync();
            var wordCollectionDtos = new List<WordCollectionDto>();
            foreach (var wordCollection in wordCollections)
            {
                var wordDto = _mapper.Map<WordCollectionDto>(wordCollection);
                wordCollectionDtos.Add(wordDto);
            }
            return wordCollectionDtos;
        }
        public async Task<IEnumerable<WordCollectionDto>> GetWordCollections()
        {
            var wordCollections = await _unitOfWork.WordCollectionRepo.GetWordCollections();
            var wordCollectionDtos = new List<WordCollectionDto>();
            foreach (var wordCollection in wordCollections)
            {
                var wordDto = _mapper.Map<WordCollectionDto>(wordCollection);
                wordCollectionDtos.Add(wordDto);
            }
            return wordCollectionDtos;
        }
        public async Task<WordCollectionDto> CreateWordCollection(WordCollectionCreateDto wordCollectionCreateDto)
        {
            var newWordCollection = new WordCollection()
            {
                Description = wordCollectionCreateDto.Description,
                Name = wordCollectionCreateDto.Name,
                CreatedDate = System.DateTimeOffset.Now,
                User_ID = wordCollectionCreateDto.User_ID
            };
            _unitOfWork.WordCollectionRepo.Create(newWordCollection);


            await _unitOfWork.SaveChangesAsync();
            var wordCollection = await GetWordCollection(newWordCollection.WordCollection_ID);
            return wordCollection;
        }

        public async Task<bool> DeleteWordCollection(long id)
        {
            var wordCollection = await _unitOfWork.WordCollectionRepo.GetWordCollection(id);
            if (wordCollection == null)
            {
                return false;
            }

            _unitOfWork.WordCollectionRepo.Delete(wordCollection);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<WordCollectionDto> UpdateWordCollection(long id, WordCollectionUpdateDto wordCollectionUpdateDto)
        {
            var wordCollection = await _unitOfWork.WordCollectionRepo
                .Find(s => s.WordCollection_ID == id)
                .AsNoTracking()
                .Include(s => s.Words).ThenInclude(s => s.WordDefinitions)
                .SingleOrDefaultAsync();

            if (wordCollection == null)
            {
                return null;
            }

            var newWordCollection = wordCollection with
            {
                Description = wordCollectionUpdateDto.Description,
                Name = wordCollectionUpdateDto.Name,
                UpdatedDate = System.DateTimeOffset.Now
            };
            _unitOfWork.WordCollectionRepo.Update(newWordCollection);

            await _unitOfWork.SaveChangesAsync();


            var wordCollectionDto = await GetWordCollection(id);
            return wordCollectionDto;
        }

        public async Task<WordCollectionDto> CreateWordsFromText(string text, long wordCollectionID)
        {
            var dictionaryWords = await GetWords(new Request() { Text = text });


            foreach (var dictionaryWord in dictionaryWords)
            {
                var readings = "";
                var meanings = "";
                var partOfSpeeches = "";
                foreach (var wordDtos in dictionaryWord.WordDtos)
                {
                    readings = string.Join(", ", wordDtos.ReadingElementsDto.First().Readings);
                    meanings = string.Join(", ", wordDtos.SensesDto.First().Glosses);
                    partOfSpeeches = string.Join(", ", wordDtos.SensesDto.First().PartOfSpeeches);
                    //foreach (var readingElementDto in wordDtos.ReadingElementsDto)
                    //{
                    //    readings = string.Join(", ", readingElementDto.Readings);
                    //}
                    //foreach (var senseDto in wordDtos.SensesDto)
                    //{
                    //    meanings = string.Join(", ", senseDto.Glosses);
                    //    partOfSpeeches = string.Join(", ", senseDto.PartOfSpeeches);
                    //}

                }


                var stage = await _unitOfWork.StageRepo.Find(s => s.StageNumber == 0).SingleOrDefaultAsync();
                var newWord = new Word()
                {
                    CreatedDate = System.DateTimeOffset.UtcNow,
                    IsInSRS = true,
                    StartCurrentSRSDate = System.DateTimeOffset.UtcNow,
                    StartInitialSRSDate = System.DateTimeOffset.UtcNow,
                    WordCollection_ID = wordCollectionID,
                    Stage_ID = stage.Stage_ID
                };
                _unitOfWork.WordRepo.Create(newWord);

                var wordDefinition = new WordDefinition()
                {
                    OriginalEntry = dictionaryWord.OriginalEntry,
                    Word = newWord
                };
                _unitOfWork.WordDefinitionRepo.Create(wordDefinition);


            }

            await _unitOfWork.SaveChangesAsync();

            var wordCollectionDto = await GetWordCollection(wordCollectionID);
            return wordCollectionDto;
        }

        public async Task<WordCollectionDto> CopyWordCollection(long wordCollectionID, long userID)
        {
            var wordCollection = await _unitOfWork.WordCollectionRepo
                .Find(s => s.WordCollection_ID == wordCollectionID)
                .Include(s => s.User)
                .Include(s => s.Words).ThenInclude(s => s.WordDefinitions)
                .Include(s => s.Words).ThenInclude(s => s.Stage)
                .SingleOrDefaultAsync();

            var newWordCollection = new WordCollection()
            {
                CreatedDate = System.DateTimeOffset.UtcNow,
                Description = wordCollection.Description,
                Name = wordCollection.Name,
                User_ID = userID,

            };
            _unitOfWork.WordCollectionRepo.Create(newWordCollection);

            var stage = await _unitOfWork.StageRepo
                .Find(s => s.StageNumber == 0)
                .SingleOrDefaultAsync();

            foreach (var word in wordCollection.Words)
            {
                var newWord = new Word()
                {
                    IsInSRS = true,
                    Stage_ID = stage.Stage_ID,
                    StartCurrentSRSDate = System.DateTimeOffset.UtcNow,
                    WordCollection = newWordCollection,
                    StartInitialSRSDate = System.DateTimeOffset.UtcNow,
                    CreatedDate = System.DateTimeOffset.UtcNow,

                };
                _unitOfWork.WordRepo.Create(newWord);
                foreach (var meaningReading in word.WordDefinitions)
                {
                    var newMeaningReading = new WordDefinition()
                    {
                        Word = newWord,
                        OriginalEntry = meaningReading.OriginalEntry,

                    };
                    _unitOfWork.WordDefinitionRepo.Create(newMeaningReading);
                }

            }

            await _unitOfWork.SaveChangesAsync();
            var wordCollectionDto = await GetWordCollection(newWordCollection.WordCollection_ID);
            return wordCollectionDto;
        }





        private async Task<IEnumerable<IEnumerable<DictionaryWord>>> GetWordLists(IEnumerable<string> words)
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

            var dictionaryWordsTask = GetWordLists(words);
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
        }
    }
}
