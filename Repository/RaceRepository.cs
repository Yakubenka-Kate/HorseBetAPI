using Contracts;
using HorseBet.Models;
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

        public IEnumerable<Race> GetAllRaces(bool trackChanges) 
            => FindAll(trackChanges).ToList();

        public Race GetRace(Guid raceId, bool trackChanges)
            => FindByCondition(r => r.Id.Equals(raceId), trackChanges).SingleOrDefault();
    }
}
