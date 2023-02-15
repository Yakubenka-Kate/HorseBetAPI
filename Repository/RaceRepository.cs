using Contracts;
using HorseBet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RaceRepository : RepositoryBase<Race>, IRaceRepository
    {
        public RaceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {}

        public void CreateRace(Race race) 
            => Create(race);

        public async Task<IEnumerable<Race>> GetAllRacesAsync(bool trackChanges) 
            => await FindAll(trackChanges).ToListAsync();

        public async Task<Race> GetRaceAsync(Guid raceId, bool trackChanges)
            => await FindByCondition(r => r.Id.Equals(raceId), trackChanges).SingleOrDefaultAsync();
    }
}
