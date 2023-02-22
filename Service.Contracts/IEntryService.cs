using Entities.Models;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ManipulationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IEntryService
    {
        Task<IEnumerable<Entry>> GetAllEntriesAsync(bool trackChanges);
        Task<Entry> GetEntryByIdAsync(Guid id, bool trackChanges);
        Task<Entry> GetEntryForHorseAsync(Guid horseId, Guid id, bool trackChanges);
        Task<IEnumerable<Entry>> GetEntriesForHorseAsync(Guid horseId, bool trackChanges);
        Task<EntryDto> CreateEntryAsync(Guid raceId, Guid horseId, EntryManipulationDto entryForCreation, bool trackChanges);
        Task CreateFullEntry(Guid raceId, IEnumerable<Guid> horseIds, bool trackChanges);
        Task DeleteEntryForHorseAsync(Guid horseId, Guid id, bool trackChanges);
        Task UpdateEntryForHorseAsync(Guid horseId, Guid id, EntryManipulationDto entryForUpdate, bool horseTrackChanges, bool entryTrackChanges);
        Task<IEnumerable<Entry>> GetResult(Guid raceIdt, bool trackChanges);
    }
}
