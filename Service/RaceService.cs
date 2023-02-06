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

        public RaceDto CreateRace(RaceForCreationDto race)
        {
            var raceEntity = _mapper.Map<Race>(race);

            _repository.Race.CreateRace(raceEntity);
            _repository.Save();

            var raceToReturn = _mapper.Map<RaceDto>(raceEntity);

            return raceToReturn;
        }

        public IEnumerable<RaceDto> GetAllRaces(bool trackChanges)
        {
            var races = _repository.Race.GetAllRaces(trackChanges);

            var racesDto = _mapper.Map<IEnumerable<RaceDto>>(races);

            return racesDto;
        }

        public Race GetRace(Guid raceId, bool trackChanges)
        {
            var race = _repository.Race.GetRace(raceId, trackChanges);

            if (race is null)
                throw new RaceNotFoundException(raceId);

            return race;
        }
    }
}
