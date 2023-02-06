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
            CreateMap<HorseForCreationDto, Horse>();

            CreateMap<Race, RaceDto>();
            CreateMap<RaceForCreationDto, Race>();

            CreateMap<Entry, EntryDto>();
            CreateMap<EntryForManipulationsDto, Entry>();

            CreateMap<Bet, BetDto>();
            CreateMap<BetForManipulationsDto, Bet>();
        }
    }
}
