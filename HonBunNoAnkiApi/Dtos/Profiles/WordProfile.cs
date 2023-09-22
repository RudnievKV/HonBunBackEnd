using AutoMapper;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Dtos.WordDtos;
using HonbunNoAnkiApi.Models;

namespace HonbunNoAnkiApi.Dtos.Profiles
{
    public class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<Word, WordDto>();
        }
    }
}
