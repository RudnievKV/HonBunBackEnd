using AutoMapper;
using DictionaryApi.Dtos.KanjiDtos;
using DictionaryApi.Models.KanjiModels;
using System.Collections.Generic;
using System.Linq;

namespace DictionaryApi.Models.Profiles
{
    public class KanjiProfile : Profile
    {
        public KanjiProfile()
        {
            CreateMap<Reading, ReadingDto>();
            CreateMap<Miscellanious, MiscellaniousDto>();

            CreateMap<Kanji, KanjiDto>()
                .ForMember(kanjiDto => kanjiDto.Miscellanious, opt => opt.MapFrom(src => src.Miscellanious))
                .ForMember(kanjiDto => kanjiDto.Meanings, opt => opt.MapFrom(src =>
                    new List<string>(
                        src.ReadingMeaning.FirstOrDefault().
                        ReadingMeaningGroups.FirstOrDefault().
                        Meanings)))
                //.ForMember(kanjiDto => kanjiDto.ReadingMeaning, opt => opt.MapFrom(src => src.ReadingMeaning
                //))
                .ForMember(kanjiDto => kanjiDto.Readings, opt => opt.MapFrom(src => src.ReadingMeaning.FirstOrDefault().ReadingMeaningGroups.FirstOrDefault().Readings));
            //.ForMember(kanjiDto => kanjiDto.Meanings, opt => opt.MapFrom(src => {  }));

        }
    }
}
