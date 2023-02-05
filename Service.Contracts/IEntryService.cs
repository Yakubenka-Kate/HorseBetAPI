using HorseBet.Models;
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
        Entry GetEntry(Guid horseId, Guid id, bool trackChanges);
        IEnumerable<Entry> GetEntriesForHorse(Guid horseId, bool trackChanges);
    }
}
