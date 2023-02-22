using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObjects;
using System;
using System.Collections;
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

        public void UpdateBet(Bet bet) => Update(bet);

        public void DeleteBet(Bet bet) => Delete(bet);

        public async Task<IEnumerable<Bet>> GetAllBetsAsunc(bool trackChanges)
            => await FindAll(trackChanges).ToListAsync();

        public async Task<Bet> GetBetForEntryAsunc(Guid entryId, Guid id, bool trackChanges)
            => await FindByCondition(b => b.EntryId.Equals(entryId) && b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task<IEnumerable<Bet>> GetBetsForEntryAsunc(Guid entryId, bool trackChanges) 
            => await FindByCondition(b => b.EntryId.Equals(entryId), trackChanges).ToListAsync();

        public async Task<IEnumerable<Bet>> GetAllBetsForUserAsync(string userId, bool trackChanges)
            => await FindByCondition(b => b.UserId.Equals(userId), trackChanges).ToListAsync();

        public async Task<double> GetRatesForEntryAsync(IEnumerable<Bet> bet, bool trackChanges)
            => await FindByCondition(b => bet.Contains(b), trackChanges).Where(b => b.BetPosition == 1).SumAsync(b => b.Rate);

    }
}
