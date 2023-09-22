using AutoMapper;
using HonbunNoAnkiApi.Dtos.UserDtos;
using HonbunNoAnkiApi.Dtos.WordCollectionDtos;
using HonbunNoAnkiApi.Models;

namespace HonbunNoAnkiApi.Dtos.Profiles
{
    public class WordCollectionProfile : Profile
    {
        public WordCollectionProfile()
        {
            CreateMap<WordCollection, WordCollectionDto>();
        }
    }
}
