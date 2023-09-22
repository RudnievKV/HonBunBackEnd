using AutoMapper;
using DictionaryApi.Dtos.WordDtos;
using DictionaryApi.Models.WordModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DictionaryApi.Models.Profiles
{
    public class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<Sense, SenseDto>();
            CreateMap<WordKanjiElement, WordKanjiElementDto>();
            CreateMap<WordReadingElement, WordReadingElementDto>();
            CreateMap<Word, WordDto>()
                .ForMember(wordDto => wordDto.SensesDto, opt => opt.MapFrom(src => src.Senses))
                .ForMember(wordDto => wordDto.KanjiElementsDto, opt => opt.MapFrom(src => src.KanjiElements))
                .ForMember(wordDto => wordDto.ReadingElementsDto, opt => opt.MapFrom(src => src.ReadingElements));

            //            .ForMember(wordDto => wordDto.SensesDto, opt => opt.MapFrom(src => src.Senses))
            //.ForMember(wordDto => wordDto.KanjiElementsDto, opt => opt.MapFrom(src =>
            //    (src.KanjiElements != null) ? src.KanjiElements : new List<WordKanjiElement>()))
            // src.Senses.FirstOrDefault().PartOfSpeeches
            //.ForPath(wordDto => wordDto.SensesDto.FirstOrDefault().Miscellaneous, opt => opt.MapFrom(src => src.Senses.FirstOrDefault().Miscellaneous))
            //.ForPath(wordDto => wordDto.SensesDto.FirstOrDefault().Dialects, opt => opt.MapFrom(src => src.Senses.FirstOrDefault().Dialects))
            //.ForPath(wordDto => wordDto.SensesDto.FirstOrDefault().Informations, opt => opt.MapFrom(src => src.Senses.FirstOrDefault().Informations))
            //.ForPath(wordDto => wordDto.SensesDto.FirstOrDefault().Glosses, opt => opt.MapFrom(src => src.Senses.FirstOrDefault().Glosses));


        }
    }
}
