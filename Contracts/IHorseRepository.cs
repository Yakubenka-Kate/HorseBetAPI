using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IHorseRepository
    {
        Task<IEnumerable<Horse>> GetAllHorsesAsync(bool trackChanges);
        Task<Horse> GetHorseAsync(Guid horseId, bool trackChanges);
        void CreateHorse(Horse horse);
        Task<IEnumerable<Horse>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteHorse(Horse horse);
    }
}
