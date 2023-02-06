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

        public EntryDto CreateEntry(Guid raceId, Guid horseId, EntryForManipulationsDto entryForCreation, bool trackChanges)
        {
            var horse = _repository.Horse.GetHorse(horseId, trackChanges);
            
            if (horse is null)
                throw new HorseNotFoundException(horseId);

            var race = _repository.Race.GetRace(raceId, trackChanges);

            if (race is null)
                throw new RaceNotFoundException(raceId);

            var entryEntity = _mapper.Map<Entry>(entryForCreation);

            _repository.Entry.CreateEntry(raceId, horseId, entryEntity);
            _repository.Save();

            var raceToReturn = _mapper.Map<EntryDto>(entryEntity);

            return raceToReturn;
        }

        public void DeleteEntryForHorse(Guid horseId, Guid id, bool trackChanges)
        {
            var horse = _repository.Horse.GetHorse(horseId, trackChanges);

            if (horse is null)
                throw new HorseNotFoundException(horseId);

            var entryForHorse = _repository.Entry.GetEntryForHorse(horseId, id, trackChanges);

            if (entryForHorse is null)
                throw new EntryNotFoundException(id);

            _repository.Entry.DeleteEntry(entryForHorse);
            _repository.Save();
        }

        public IEnumerable<Entry> GetAllEntries(bool trackChanges)
        {
            var entries = _repository.Entry.GetAllEntries(trackChanges);

            return entries;
        }

        public IEnumerable<Entry> GetEntriesForHorse(Guid horseId, bool trackChanges)
        {
            var horse = _repository.Horse.GetHorse(horseId, trackChanges);

            if (horse is null)
                throw new HorseNotFoundException(horseId);

            var entries = _repository.Entry.GetEntriesForHorse(horseId, trackChanges);

            return entries;
        }

        public Entry GetEntryById(Guid id, bool trackChanges)
        {
            var entry = _repository.Entry.GetEntryById(id, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(id);

            return entry;
        }

        public Entry GetEntryForHorse(Guid horseId, Guid id, bool trackChanges)
        {
            var horse = _repository.Horse.GetHorse(horseId, trackChanges);

            if(horse is null)
                throw new HorseNotFoundException(horseId);

            var entry = _repository.Entry.GetEntryForHorse(horseId, id, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(id);

            return entry;
        }

        public void UpdateEntryForHorse(Guid horseId, Guid id, EntryForManipulationsDto entryForUpdate, 
            bool horseTrackChanges, bool entryTrackChanges)
        {
            var horse = _repository.Horse.GetHorse(horseId, horseTrackChanges);

            if (horse is null)
                throw new HorseNotFoundException(horseId);

            var entryEntity = _repository.Entry.GetEntryForHorse(horseId, id, entryTrackChanges);

            if (entryEntity is null)
                throw new EntryNotFoundException(id);

            _mapper.Map(entryForUpdate, entryEntity);
            _repository.Save();

        }
    }
}
