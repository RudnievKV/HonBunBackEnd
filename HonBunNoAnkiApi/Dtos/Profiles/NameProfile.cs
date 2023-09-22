using AutoMapper;
using HonbunNoAnkiApi.Dtos.DictionaryDtos.NameDtos;
using HonbunNoAnkiApi.Dtos.WordDtos;
using HonbunNoAnkiApi.Models.DictionaryModels.NameModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace HonbunNoAnkiApi.Dtos.Profiles
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
