using AutoMapper;
using HonbunNoAnkiApi.Dtos.StageDtos;
using HonbunNoAnkiApi.Dtos.WordDefinitionDtos;
using HonbunNoAnkiApi.Models;
using HonbunNoAnkiApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HonbunNoAnkiApi.Services
{
    public class WordDefinitionService : IWordDefinitionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WordDefinitionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<WordDefinitionDto> GetWordDefinition(long id)
        {
            var meaningReading = await _unitOfWork.WordDefinitionRepo.GetWordDefinition(id);
            var meaningReadingDto = _mapper.Map<WordDefinitionDto>(meaningReading);
            return meaningReadingDto;
        }

        public async Task<IEnumerable<WordDefinitionDto>> GetWordDefinitions()
        {
            var meaningReadings = await _unitOfWork.WordDefinitionRepo.GetWordDefinitions();
            var meaningReadingDtos = new List<WordDefinitionDto>();
            foreach (var meaningReading in meaningReadings)
            {
                var meaningReadingDto = _mapper.Map<WordDefinitionDto>(meaningReading);
                meaningReadingDtos.Add(meaningReadingDto);
            }
            return meaningReadingDtos;
        }
        public async Task<WordDefinitionDto> CreateWordDefinition(WordDefinitionCreateDto wordDefinitionCreateDto)
        {
            
            var newWordDefinition = new WordDefinition()
            {
                Word_ID = wordDefinitionCreateDto.Word_ID,
                OriginalEntry = wordDefinitionCreateDto.OriginalEntry,
            };
            var newReading = new Reading()
            {
                Value = wordDefinitionCreateDto.Reading.Value,
                WordDefinition = newWordDefinition
            };
            _unitOfWork.ReadingRepo.Create(newReading);
            _unitOfWork.WordDefinitionRepo.Create(newWordDefinition);

            foreach(var meaning in wordDefinitionCreateDto.Meanings)
            {
                var newMeaning = new Meaning()
                {
                    PartOfSpeech= meaning.PartOfSpeech,
                    WordDefinition = newWordDefinition,

                };
                foreach (var meaningValue in meaning.Meanings)
                {
                    var newMeaningValue = new MeaningValue()
                    {
                        Value = meaningValue,
                        Meaning = newMeaning,
                    };
                    _unitOfWork.MeaningValueRepo.Create(newMeaningValue);
                }
                _unitOfWork.MeaningRepo.Create(newMeaning);
            }

            await _unitOfWork.SaveChangesAsync();

            var wordDefinitionDto = await GetWordDefinition(newWordDefinition.WordDefinition_ID);
            return wordDefinitionDto;
        }

        public async Task<bool> DeleteWordDefinition(long id)
        {
            var wordDefinition = await _unitOfWork.WordDefinitionRepo.GetWordDefinition(id);
            if (wordDefinition == null)
            {
                return false;
            }

            _unitOfWork.WordDefinitionRepo.Delete(wordDefinition);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<WordDefinitionDto> UpdateWordDefinition(long id, WordDefinitionUpdateDto wordDefinitionUpdateDto)
        {
            var wordDefinition = await _unitOfWork.WordDefinitionRepo
                .Find(s => s.WordDefinition_ID == id)
                .Include(s => s.Reading)
                .Include(s => s.Meanings).ThenInclude(s => s.MeaningValues)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (wordDefinition == null)
            {
                return null;
            }

            _unitOfWork.WordDefinitionRepo.Delete(wordDefinition);

            var newReading = new Reading()
            {
                Value = wordDefinitionUpdateDto.Reading.Value,
            };
            _unitOfWork.ReadingRepo.Create(newReading);


            foreach(var meaning in wordDefinitionUpdateDto.Meanings)
            {
                var newMeaning = new Meaning()
                {
                    PartOfSpeech= meaning.PartOfSpeech,
                    WordDefinition_ID= id,
                };
                _unitOfWork.MeaningRepo.Create(newMeaning);
                foreach(var meaningValue in meaning.Meanings)
                {
                    var newMeaningValue = new MeaningValue()
                    {
                        Meaning = newMeaning,
                        Value = meaningValue
                    };
                    _unitOfWork.MeaningValueRepo.Create(newMeaningValue);
                }
            }

            var newWordDefinition = new WordDefinition()
            {
                Word_ID = wordDefinitionUpdateDto.Word_ID,
                WordDefinition_ID = id,
                OriginalEntry = wordDefinitionUpdateDto.OriginalEntry,
            };

            _unitOfWork.WordDefinitionRepo.Update(newWordDefinition);

            await _unitOfWork.SaveChangesAsync();


            var wordDefinitionDto = await GetWordDefinition(id);
            return wordDefinitionDto;
        }
    }
}
