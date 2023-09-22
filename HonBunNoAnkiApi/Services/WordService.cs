using AutoMapper;
using HonbunNoAnki.DBContext;
using HonbunNoAnki.Models;
using HonbunNoAnkiApi.Dtos.DictionaryDtos.WordDtos;
using HonbunNoAnkiApi.Dtos.MeaningReadingDtos;
using HonbunNoAnkiApi.Dtos.StageDtos;
using HonbunNoAnkiApi.Dtos.WordDtos;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MonteNegRo.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services
{
    public class WordService : IWordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WordService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<WordDto> GetWord(long id)
        {
            var word = await _unitOfWork.WordRepo
                .Find(s => s.Word_ID == id)
                .Include(s => s.MeaningReadings)
                .Include(s => s.Stage)
                .SingleOrDefaultAsync();
            var wordDto = _mapper.Map<WordDto>(word);
            return wordDto;
        }
        public async Task<IEnumerable<WordDto>> GetWords()
        {
            var words = await _unitOfWork.WordRepo.GetWords();
            var wordDtos = new List<WordDto>();
            foreach (var word in words)
            {
                var wordDto = _mapper.Map<WordDto>(word);
                wordDtos.Add(wordDto);
            }
            return wordDtos;
        }
        public async Task<WordDto> CreateWord(WordCreateDto wordCreateDto)
        {
            var newWord = new Word()
            {
                IsInSRS = true,
                WordCollection_ID = wordCreateDto.WordCollection_ID,
                Stage_ID = wordCreateDto.Stage_ID,
                StartCurrentSRSDate = System.DateTimeOffset.UtcNow,
                StartInitialSRSDate = System.DateTimeOffset.UtcNow,
                CreatedDate = System.DateTimeOffset.UtcNow,
            };
            _unitOfWork.WordRepo.Create(newWord);

            foreach (var definition in wordCreateDto.Definitions)
            {
                var newMeaningReading = new MeaningReading()
                {
                    Meaning = definition.Meaning,
                    PartOfSpeech = definition.PartOfSpeech,
                    Reading = definition.Reading,
                    OriginalEntry = definition.OriginalEntry,
                    Word = newWord
                };
                _unitOfWork.MeaningReadingRepo.Create(newMeaningReading);
            }
            await _unitOfWork.SaveChangesAsync();


            var wordDto = await GetWord(newWord.Word_ID);
            return wordDto;
        }
        public async Task<bool> DeleteWord(long id)
        {
            var word = await _unitOfWork.WordRepo.GetWord(id);
            if (word == null)
            {
                return false;
            }

            _unitOfWork.WordRepo.Delete(word);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        public async Task<WordDto> UpdateWord(long id, WordUpdateDto wordUpdateDto)
        {
            var word = await _unitOfWork.WordRepo
                .Find(s => s.Word_ID == id)
                .AsNoTracking()
                .Include(s => s.MeaningReadings)
                .SingleOrDefaultAsync();

            if (word == null)
            {
                return null;
            }


            var newWord = word with
            {
                IsInSRS = true,
                WordCollection_ID = wordUpdateDto.WordCollection_ID,
                Stage_ID = wordUpdateDto.Stage_ID,

            };

            _unitOfWork.WordRepo.Update(newWord);

            foreach (var readingMeaning in word.MeaningReadings)
            {
                _unitOfWork.MeaningReadingRepo.Delete(readingMeaning);
            }

            foreach (var definition in wordUpdateDto.Definitions)
            {
                var newMeaningReading = new MeaningReading()
                {
                    Meaning = definition.Meaning,
                    PartOfSpeech = definition.PartOfSpeech,
                    Reading = definition.Reading,
                    Word = newWord,
                    OriginalEntry = definition.OriginalEntry,
                };
                _unitOfWork.MeaningReadingRepo.Create(newMeaningReading);
            }

            await _unitOfWork.SaveChangesAsync();


            var wordDto = await GetWord(id);
            return wordDto;
        }

        public async Task<IEnumerable<WordDto>> GetSRSWords(long userID)
        {

            var words = await _unitOfWork.WordRepo
                .Find(s => s.IsInSRS == true && s.StartCurrentSRSDate.Value.AddSeconds(s.Stage.Duration) <= System.DateTimeOffset.Now && s.WordCollection.User_ID == userID)
                .Include(s => s.MeaningReadings)
                .Include(s => s.Stage)
                .ToListAsync();

            var wordDtos = new List<WordDto>();
            foreach (var word in words)
            {
                var wordDto = _mapper.Map<WordDto>(word);
                wordDtos.Add(wordDto);
            }
            return wordDtos;
        }

        public async Task<WordDto> UpdateWordBasedOnReview(long id, WordUpdateReviewDto wordUpdateReviewDto, long userID)
        {
            var word = await _unitOfWork.WordRepo
                .Find(s => s.Word_ID == id)
                .AsNoTracking()
                .Include(s => s.MeaningReadings)
                .Include(s => s.Stage)
                .SingleOrDefaultAsync();

            if (word == null)
            {
                return null;
            }

            if (wordUpdateReviewDto.IsAnswerCorrect)
            {
                var nextStage = await _unitOfWork.StageRepo
                .Find(s => s.StageNumber == word.Stage.StageNumber + 1)
                .SingleOrDefaultAsync();

                if (nextStage == null)
                {
                    return null;
                }

                var user = await _unitOfWork.UserRepo.GetUser(userID);
                var newUser = new User()
                {
                    User_ID = userID,
                    CurrentExperience = user.CurrentExperience + 40,
                    Email = user.Email,
                    CreatedDate = user.CreatedDate,
                    PasswordHash = user.PasswordHash,
                    Username = user.Username,
                    UpdatedDate = user.UpdatedDate
                };
                _unitOfWork.UserRepo.Update(newUser);
                var newWord = new Word
                {
                    IsInSRS = true,
                    CreatedDate = word.CreatedDate,
                    StartCurrentSRSDate = System.DateTimeOffset.UtcNow,
                    StartInitialSRSDate = word.StartInitialSRSDate,
                    WordCollection_ID = word.WordCollection_ID,
                    Word_ID = word.Word_ID,
                    UpdatedDate = word.UpdatedDate,
                    Stage_ID = nextStage.Stage_ID
                };
                _unitOfWork.WordRepo.Update(newWord);
                await _unitOfWork.SaveChangesAsync();

                var wordDto = await GetWord(id);
                return wordDto;
            }
            else
            {
                var nextStage = await _unitOfWork.StageRepo
                .Find(s => s.StageNumber == word.Stage.StageNumber - 2)
                .SingleOrDefaultAsync();

                if (nextStage == null)
                {
                    var user = await _unitOfWork.UserRepo.GetUser(userID);
                    var newUser = new User()
                    {
                        User_ID = userID,
                        CurrentExperience = user.CurrentExperience + 10,
                        Email = user.Email,
                        CreatedDate = user.CreatedDate,
                        PasswordHash = user.PasswordHash,
                        Username = user.Username,
                        UpdatedDate = user.UpdatedDate
                    };

                    _unitOfWork.UserRepo.Update(newUser);
                    var newWord = new Word
                    {
                        IsInSRS = true,
                        CreatedDate = word.CreatedDate,
                        StartCurrentSRSDate = System.DateTimeOffset.UtcNow,
                        StartInitialSRSDate = word.StartInitialSRSDate,
                        WordCollection_ID = word.WordCollection_ID,
                        Word_ID = word.Word_ID,
                        UpdatedDate = word.UpdatedDate,
                        Stage_ID = 1
                    };
                    _unitOfWork.WordRepo.Update(newWord);
                    await _unitOfWork.SaveChangesAsync();

                    var wordDto = await GetWord(id);
                    return wordDto;
                }
                else
                {
                    var user = await _unitOfWork.UserRepo.GetUser(userID);
                    var newUser = new User()
                    {
                        User_ID = userID,
                        CurrentExperience = user.CurrentExperience + 10,
                        Email = user.Email,
                        CreatedDate = user.CreatedDate,
                        PasswordHash = user.PasswordHash,
                        Username = user.Username,
                        UpdatedDate = user.UpdatedDate
                    };
                    _unitOfWork.UserRepo.Update(newUser);
                    var newWord = new Word
                    {
                        IsInSRS = true,
                        CreatedDate = word.CreatedDate,
                        StartCurrentSRSDate = System.DateTimeOffset.UtcNow,
                        StartInitialSRSDate = word.StartInitialSRSDate,
                        WordCollection_ID = word.WordCollection_ID,
                        Word_ID = word.Word_ID,
                        UpdatedDate = word.UpdatedDate,
                        Stage_ID = nextStage.Stage_ID
                    };
                    _unitOfWork.WordRepo.Update(newWord);
                    await _unitOfWork.SaveChangesAsync();

                    var wordDto = await GetWord(id);
                    return wordDto;
                }
            }


        }
    }
}
