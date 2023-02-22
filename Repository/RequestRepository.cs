using Contracts;
using Entities.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RequestRepository : IRequestRepository
    {
        protected RepositoryContext RepositoryContext;

        public RequestRepository(RepositoryContext repositoryContext) => RepositoryContext = repositoryContext;

        public double GetAllRatesForRaceId(Guid raceId)
        {
            var sql = RepositoryContext.Entries
                .Join(RepositoryContext.Bets,
                    e => e.Id,
                    b => b.EntryId,
                    (e, b) => new { e.RaceId, e.Result, b.Rate, b.BetPosition })
                .Where(r => r.RaceId == raceId)
                .Sum(t => t.Rate);

            return sql;
        }

        public double GetAllWinRatesForRaceId(Guid raceId)
        {
            var sql = RepositoryContext.Entries
                .Join(RepositoryContext.Bets, 
                    e => e.Id,
                    b => b.EntryId,
                    (e, b) => new { e.RaceId, e.Result, b.Rate, b.BetPosition })
                .Where(r => r.RaceId == raceId && r.Result == r.BetPosition)
                .Sum(t => t.Rate);

            return sql;
        }

    }
}
