using HorseBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEntryRepository
    {
        IEnumerable<Entry> GetAllEntries(bool trackChanges);
        Entry GetEntryById(Guid id, bool trackChanges);
        Entry GetEntryForHorse(Guid horseId, Guid id, bool trackChanges);
        IEnumerable<Entry> GetEntriesForHorse(Guid horseId, bool trackChanges);
        void CreateEntry(Guid raceId, Guid horseId, Entry entry);
        void DeleteEntry(Entry entry);

    }
}
