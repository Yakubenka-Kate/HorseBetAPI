using Contracts;
using HorseBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BetRepository : RepositoryBase<Bet>, IBetRepository
    {
        public BetRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBet(Guid entryId, Bet bet)
        {
            bet.EntryId = entryId;
            Create(bet);
        }

        public void DeleteBet(Bet bet) => Delete(bet);

        public IEnumerable<Bet> GetAllBets(bool trackChanges)
            => FindAll(trackChanges).ToList();

        public Bet GetBetForEntry(Guid entryId, Guid id, bool trackChanges)
            => FindByCondition(b => b.EntryId.Equals(entryId) && b.Id.Equals(id), trackChanges).SingleOrDefault();

        public IEnumerable<Bet> GetBetsForEntry(Guid entryId, bool trackChanges) => FindByCondition(b => b.EntryId.Equals(entryId), trackChanges).ToList();
    }
}
