using AutoMapper;
using DictionaryApi.Dtos.NameDtos;
using DictionaryApi.Dtos.WordDtos;
using DictionaryApi.Models.NameModels;
using DictionaryApi.Models.WordModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace DictionaryApi.Models.Profiles
{
    public class NameProfile : Profile
    {
        public NameProfile()
        {
            CreateMap<NameKanjiElement, NameKanjiElementDto>();
            CreateMap<NameReadingElement, NameReadingElementDto>();
            CreateMap<Translation, TranslationDto>();
            CreateMap<Name, NameDto>()
                .ForMember(nameDto => nameDto.KanjiElement, opt => opt.MapFrom(src => src.KanjiElement))
                .ForMember(nameDto => nameDto.ReadingElement, opt => opt.MapFrom(src => src.ReadingElement))
                .ForMember(nameDto => nameDto.Translation, opt => opt.MapFrom(src => src.Translation));

        }
    }
}
