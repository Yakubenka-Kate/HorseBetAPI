using HorseBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IHorseRepository
    {
        IEnumerable<Horse> GetAllHorses(bool trackChanges);
        Horse GetHorse(Guid horseId, bool trackChanges);
        void CreateHorse(Horse horse);
        IEnumerable<Horse> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteHorse(Horse horse);
    }
}
