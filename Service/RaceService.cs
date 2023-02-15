using AutoMapper;
using Contracts;
using Entities.Exceptions;
using HorseBet.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class RaceService : IRaceService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public RaceService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<RaceDto> CreateRaceAsync(RaceManipulationDto race)
        {
            var raceEntity = _mapper.Map<Race>(race);

            _repository.Race.CreateRace(raceEntity);
            await _repository.SaveAsync();

            var raceToReturn = _mapper.Map<RaceDto>(raceEntity);

            return raceToReturn;
        }

        public async Task<IEnumerable<RaceDto>> GetAllRacesAsync(bool trackChanges)
        {
            var races = await _repository.Race.GetAllRacesAsync(trackChanges);

            var racesDto = _mapper.Map<IEnumerable<RaceDto>>(races);

            return racesDto;
        }

        public async Task<Race> GetRaceAsync(Guid raceId, bool trackChanges)
        {
            var race = await _repository.Race.GetRaceAsync(raceId, trackChanges);
           
            if (race is null)
                throw new RaceNotFoundException(raceId);

            return race;
        }
    }
}
