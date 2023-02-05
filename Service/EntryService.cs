using Contracts;
using Entities.Exceptions;
using HorseBet.Models;
using Service.Contracts;
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

        public EntryService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
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

        public Entry GetEntry(Guid horseId, Guid id, bool trackChanges)
        {
            var horse = _repository.Horse.GetHorse(horseId, trackChanges);

            if(horse is null)
                throw new HorseNotFoundException(horseId);

            var entry = _repository.Entry.GetEntry(horseId, id, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(id);

            return entry;
        }
    }
}
