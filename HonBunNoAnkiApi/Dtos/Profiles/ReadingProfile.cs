using AutoMapper;
using HonbunNoAnkiApi.Dtos.ReadingDtos;
using HonbunNoAnkiApi.Dtos.StageDtos;
using HonbunNoAnkiApi.Models;

namespace HonbunNoAnkiApi.Dtos.Profiles
{
    public class ReadingProfile : Profile
    {
        public ReadingProfile() 
        {
            CreateMap<Reading, ReadingDto>();
        }
    }
}
