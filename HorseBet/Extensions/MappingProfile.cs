using AutoMapper;
using HorseBet.Models;
using Shared.DataTransferObjects;

namespace HorseBet.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Horse, HorseDto>();
            CreateMap<HorseForCreation, Horse>();
        }
    }
}
