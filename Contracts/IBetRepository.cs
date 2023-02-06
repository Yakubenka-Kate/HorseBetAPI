using HorseBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBetRepository
    {
        IEnumerable<Bet> GetAllBets(bool trackChanges);
        Bet GetBetForEntry(Guid entryId, Guid id, bool trackChanges);
        IEnumerable<Bet> GetBetsForEntry(Guid entryId, bool trackChanges);
        void CreateBet(Guid entryId, Bet bet);
        void DeleteBet(Bet bet);
    }
}
