using AutoMapper;
using HonbunNoAnkiApi.Dtos.MeaningDtos;
using HonbunNoAnkiApi.Dtos.StageDtos;
using HonbunNoAnkiApi.Models;

namespace HonbunNoAnkiApi.Dtos.Profiles
{
    public class MeaningProfile : Profile
    {
        public MeaningProfile() 
        {
            CreateMap<Meaning, MeaningDto>();
        }
    }
}
