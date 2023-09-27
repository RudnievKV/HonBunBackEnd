using AutoMapper;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Dtos.WordDefinitionDtos;
using HonbunNoAnkiApi.Models;

namespace HonbunNoAnkiApi.Dtos.Profiles
{
    public class WordDefinitionProfile : Profile
    {
        public WordDefinitionProfile()
        {
            CreateMap<WordDefinition, WordDefinitionDto>();
        }
    }
}
