using AutoMapper;
using HonbunNoAnkiApi.Dtos.StageDtos;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Models;

namespace HonbunNoAnkiApi.Dtos.Profiles
{
    public class StageProfile : Profile
    {
        public StageProfile()
        {
            CreateMap<Stage, StageDto>();
        }
    }
}
