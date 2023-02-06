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

        public HorseDto CreateHorse(HorseForCreationDto horse)
        {
            var horseEntity = _mapper.Map<Horse>(horse);

            _repository.Horse.CreateHorse(horseEntity);
            _repository.Save();

            var horseToReturn = _mapper.Map<HorseDto>(horseEntity);

            return horseToReturn;
        }

        public (IEnumerable<HorseDto> horses, string ids) CreateHorsesCollection(IEnumerable<HorseForCreationDto> horseCollection)
        {
            if (horseCollection is null)
                throw new HorseCollectionBadRequest();

            var horseEntities = _mapper.Map<IEnumerable<Horse>>(horseCollection);
            foreach (var horse in horseEntities)
            {
                _repository.Horse.CreateHorse(horse);
            }

            _repository.Save();

            var horseCollectionToReturn = _mapper.Map<IEnumerable<HorseDto>>(horseEntities);

            var ids = string.Join(",", horseCollectionToReturn.Select(h => h.Id));

            return (horses: horseCollectionToReturn, ids: ids);

        }

        public void DeleteHorse(Guid horseId, bool trackChanges)
        {
            var horse = _repository.Horse.GetHorse(horseId, trackChanges);

            if (horse is null)
                throw new HorseNotFoundException(horseId);

            _repository.Horse.DeleteHorse(horse);
            _repository.Save();
        }

        public IEnumerable<HorseDto> GetAllHorses(bool trackChanges)
        {
            var horses = _repository.Horse.GetAllHorses(trackChanges);

            var horsesDto = _mapper.Map<IEnumerable<HorseDto>>(horses);

            return horsesDto;
        }

        public IEnumerable<HorseDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var horseEntities = _repository.Horse.GetByIds(ids, trackChanges);

            if(ids.Count() != horseEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var horsesToReturn = _mapper.Map<IEnumerable<HorseDto>>(horseEntities);

            return horsesToReturn;
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
