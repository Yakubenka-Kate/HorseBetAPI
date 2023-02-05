using Contracts;
using HorseBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class HorseRepository : RepositoryBase<Horse>, IHorseRepository
    {
        public HorseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateHorse(Horse horse) => Create(horse);

        public IEnumerable<Horse> GetAllHorses(bool trackChanges) => 
            FindAll(trackChanges).ToList();

        public Horse GetHorse(Guid horseId, bool trackChanges) =>
            FindByCondition(e => e.Id.Equals(horseId), trackChanges).SingleOrDefault();

    }
}
