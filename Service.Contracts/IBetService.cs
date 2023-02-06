using HorseBet.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IBetService
    {
        IEnumerable<Bet> GetAllBets(bool trackChanges);
        Bet GetBetForEntry(Guid entryId, Guid id, bool trackChanges);
        IEnumerable<Bet> GetBetsForEntry(Guid entryId, bool trackChanges);
        BetDto CreateBet(Guid entryId, BetForManipulationsDto betForCreation, bool trackChanges);
        void DeleteBetForEntry(Guid entryId, Guid id, bool trackChanges);
        void UpdateBetForEntry(Guid entryId, Guid id, BetForManipulationsDto betForUpdate, bool entryTrackChanges, bool betTrackChanges);
    }
}
