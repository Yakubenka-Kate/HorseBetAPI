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

        public async Task<HorseDto> CreateHorseAsync(HorseManipulationDto horse)
        {
            var horseEntity = _mapper.Map<Horse>(horse);

            _repository.Horse.CreateHorse(horseEntity);
            await _repository.SaveAsync();

            var horseToReturn = _mapper.Map<HorseDto>(horseEntity);

            return horseToReturn;
        }

        public async Task<(IEnumerable<HorseDto> horses, string ids)> CreateHorsesCollectionAsync(IEnumerable<HorseManipulationDto> horseCollection)
        {
            if (horseCollection is null)
                throw new HorseCollectionBadRequest();

            var horseEntities = _mapper.Map<IEnumerable<Horse>>(horseCollection);
            foreach (var horse in horseEntities)
            {
                _repository.Horse.CreateHorse(horse);
            }

            await _repository.SaveAsync();

            var horseCollectionToReturn = _mapper.Map<IEnumerable<HorseDto>>(horseEntities);

            var ids = string.Join(",", horseCollectionToReturn.Select(h => h.Id));

            return (horses: horseCollectionToReturn, ids: ids);

        }

        public async Task DeleteHorseAsync(Guid horseId, bool trackChanges)
        {
            var horse = await _repository.Horse.GetHorseAsync(horseId, trackChanges);

            if (horse is null)
                throw new HorseNotFoundException(horseId);

            _repository.Horse.DeleteHorse(horse);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<HorseDto>> GetAllHorsesAsync(bool trackChanges)
        {
            var horses = await _repository.Horse.GetAllHorsesAsync(trackChanges);

            var horsesDto = _mapper.Map<IEnumerable<HorseDto>>(horses);

            return horsesDto;
        }

        public async Task<IEnumerable<HorseDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var horseEntities = await _repository.Horse.GetByIdsAsync(ids, trackChanges);

            if(ids.Count() != horseEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var horsesToReturn = _mapper.Map<IEnumerable<HorseDto>>(horseEntities);

            return horsesToReturn;
        }

        public async Task<Horse> GetHorseAsync(Guid horseId, bool trackChanges)
        {
            var horse = await _repository.Horse.GetHorseAsync(horseId, trackChanges);

            if (horse is null)               
                throw new HorseNotFoundException(horseId);

            return horse;
        }
    }
}
