using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ManipulationDto;

namespace HorseBet.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Horse, HorseDto>();
            CreateMap<HorseManipulationDto, Horse>();

            CreateMap<Race, RaceDto>();
            CreateMap<RaceManipulationDto, Race>();

            CreateMap<Entry, EntryDto>();
            CreateMap<EntryManipulationDto, Entry>();

            CreateMap<Bet, BetDto>();
            CreateMap<BetManipulationDto, Bet>();

            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
