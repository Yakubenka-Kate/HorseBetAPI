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
        Task<IEnumerable<Bet>> GetAllBetsAsunc(bool trackChanges);
        Task<Bet> GetBetForEntryAsunc(Guid entryId, Guid id, bool trackChanges);
        Task<IEnumerable<Bet>> GetBetsForEntryAsunc(Guid entryId, bool trackChanges);
        void CreateBet(Guid entryId, Bet bet);
        void DeleteBet(Bet bet);
        void UpdateBet(Bet bet);
    }
}
