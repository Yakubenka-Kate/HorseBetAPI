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
    internal class EntryService : IEntryService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EntryService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<EntryDto> CreateEntryAsync(Guid raceId, Guid horseId, EntryManipulationDto entryForCreation, bool trackChanges)
        {
            var horse = await _repository.Horse.GetHorseAsync(horseId, trackChanges);
            
            if (horse is null)
                throw new HorseNotFoundException(horseId);

            var race = await _repository.Race.GetRaceAsync(raceId, trackChanges);

            if (race is null)
                throw new RaceNotFoundException(raceId);

            var entryEntity = _mapper.Map<Entry>(entryForCreation);

            _repository.Entry.CreateEntry(raceId, horseId, entryEntity);
            await _repository.SaveAsync();

            var raceToReturn = _mapper.Map<EntryDto>(entryEntity);

            return raceToReturn;
        }

        public async Task DeleteEntryForHorseAsync(Guid horseId, Guid id, bool trackChanges)
        {
            var horse = await _repository.Horse.GetHorseAsync(horseId, trackChanges);

            if (horse is null)
                throw new HorseNotFoundException(horseId);

            var entryForHorse = await _repository.Entry.GetEntryForHorseAsync(horseId, id, trackChanges);

            if (entryForHorse is null)
                throw new EntryNotFoundException(id);

            _repository.Entry.DeleteEntry(entryForHorse);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<Entry>> GetAllEntriesAsync(bool trackChanges)
        {
            var entries = await _repository.Entry.GetAllEntriesAsync(trackChanges);

            return entries;
        }

        public async Task<IEnumerable<Entry>> GetEntriesForHorseAsync(Guid horseId, bool trackChanges)
        {
            var horse = await _repository.Horse.GetHorseAsync(horseId, trackChanges);

            if (horse is null)
                throw new HorseNotFoundException(horseId);

            var entries = await _repository.Entry.GetEntriesForHorseAsync(horseId, trackChanges);

            return entries;
        }

        public async Task<Entry> GetEntryByIdAsync(Guid id, bool trackChanges)
        {
            var entry = await _repository.Entry.GetEntryByIdAsync(id, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(id);

            return entry;
        }

        public async Task<Entry> GetEntryForHorseAsync(Guid horseId, Guid id, bool trackChanges)
        {
            var horse = await _repository.Horse.GetHorseAsync(horseId, trackChanges);

            if(horse is null)
                throw new HorseNotFoundException(horseId);

            var entry = await _repository.Entry.GetEntryForHorseAsync(horseId, id, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(id);

            return entry;
        }

        public async Task UpdateEntryForHorseAsync(Guid horseId, Guid id, EntryManipulationDto entryForUpdate, 
            bool horseTrackChanges, bool entryTrackChanges)
        {
            var horse = await _repository.Horse.GetHorseAsync(horseId, horseTrackChanges);

            if (horse is null)
                throw new HorseNotFoundException(horseId);

            var entryEntity = await _repository.Entry.GetEntryForHorseAsync(horseId, id, entryTrackChanges);

            if (entryEntity is null)
                throw new EntryNotFoundException(id);

            _mapper.Map(entryForUpdate, entryEntity);
            await _repository.SaveAsync();

        }
    }
}
