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
    internal sealed class HorseService : IHorseService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;  

        public HorseService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public HorseDto CreateHorse(HorseForCreation horse)
        {
            var horseEntity = _mapper.Map<Horse>(horse);

            _repository.Horse.CreateHorse(horseEntity);
            _repository.Save();

            var horseToReturn = _mapper.Map<HorseDto>(horseEntity);

            return horseToReturn;
        }

        public IEnumerable<HorseDto> GetAllHorses(bool trackChanges)
        {
            var horses = _repository.Horse.GetAllHorses(trackChanges);

            var horsesDto = _mapper.Map<IEnumerable<HorseDto>>(horses);

            return horsesDto;
        }

        public Horse GetHorse(Guid horseId, bool trackChanges)
        {
            var horse = _repository.Horse.GetHorse(horseId, trackChanges);

            if (horse is null)               
                throw new HorseNotFoundException(horseId);

            return horse;
        }
    }
}
