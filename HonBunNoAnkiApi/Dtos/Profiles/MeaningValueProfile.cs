using AutoMapper;
using HonbunNoAnkiApi.Dtos.MeaningValueDtos;
using HonbunNoAnkiApi.Dtos.StageDtos;
using HonbunNoAnkiApi.Models;

namespace HonbunNoAnkiApi.Dtos.Profiles
{
    public class MeaningValueProfile : Profile
    {
        public MeaningValueProfile() 
        {
            CreateMap<MeaningValue, MeaningValueDto>();
        }
    }
}
