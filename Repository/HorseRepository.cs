using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

        public void DeleteHorse(Horse horse) => Delete(horse);

        public async Task<IEnumerable<Horse>> GetAllHorsesAsync(bool trackChanges) => 
            await FindAll(trackChanges).ToListAsync();

        public async Task<IEnumerable<Horse>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
            => await FindByCondition(h => ids.Contains(h.Id), trackChanges).ToListAsync();

        public async Task<Horse> GetHorseAsync(Guid horseId, bool trackChanges) =>
            await FindByCondition(e => e.Id.Equals(horseId), trackChanges).SingleOrDefaultAsync();

    }
}
