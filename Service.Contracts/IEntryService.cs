using HorseBet.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IEntryService
    {
        IEnumerable<Entry> GetAllEntries(bool trackChanges);
        Entry GetEntryById(Guid id, bool trackChanges);
        Entry GetEntryForHorse(Guid horseId, Guid id, bool trackChanges);
        IEnumerable<Entry> GetEntriesForHorse(Guid horseId, bool trackChanges);
        EntryDto CreateEntry(Guid raceId, Guid horseId, EntryForManipulationsDto entryForCreation, bool trackChanges);
        void DeleteEntryForHorse(Guid horseId, Guid id, bool trackChanges);
        void UpdateEntryForHorse(Guid horseId, Guid id, EntryForManipulationsDto entryForUpdate, bool horseTrackChanges, bool entryTrackChanges);
    }
}
