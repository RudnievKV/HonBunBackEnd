using AutoMapper;
using HonbunNoAnkiApi.Dtos.MeaningReadingDtos;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Models;

namespace HonbunNoAnkiApi.Dtos.Profiles
{
    public class MeaningReadingProfile : Profile
    {
        public MeaningReadingProfile()
        {
            CreateMap<MeaningReading, MeaningReadingDto>();
        }
    }
}
