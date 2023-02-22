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
    public interface IBetService
    {
        Task<IEnumerable<Bet>> GetAllBetsAsync(bool trackChanges);
        Task<Bet> GetBetForEntryAsync(Guid entryId, Guid id, bool trackChanges);
        Task<IEnumerable<Bet>> GetBetsForEntryAsync(Guid entryId, bool trackChanges);
        Task<BetDto> CreateBetAsync(Guid entryId, BetManipulationDto betForCreation, bool trackChanges);
        Task DeleteBetForEntryAsync(Guid entryId, Guid id, bool trackChanges);
        Task UpdateBetForEntryAsync(Guid entryId, Guid id, BetManipulationDto betForUpdate, bool entryTrackChanges, bool betTrackChanges);
        Task<IEnumerable<Bet>> GetAllBetsForUserAsync(string userId, bool trackChanges);
    }
}
