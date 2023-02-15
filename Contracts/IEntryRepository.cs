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
        Task<IEnumerable<Entry>> GetAllEntriesAsync(bool trackChanges);
        Task<Entry> GetEntryByIdAsync(Guid id, bool trackChanges);
        Task<Entry> GetEntryForHorseAsync(Guid horseId, Guid id, bool trackChanges);
        Task<IEnumerable<Entry>> GetEntriesForHorseAsync(Guid horseId, bool trackChanges);
        Task<IEnumerable<Entry>> GetEntriesForRaceAsync(Guid raceId, bool trackChanges);
        //void CreateByIds(IEnumerable<Guid> ids, bool trackChanges);
        void CreateEntry(Guid raceId, Guid horseId, Entry entry);
        void DeleteEntry(Entry entry);
        void UpdateEntry(Entry entry);

    }
}
