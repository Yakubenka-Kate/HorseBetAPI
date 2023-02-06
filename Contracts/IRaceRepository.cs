using HorseBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRaceRepository
    {
        IEnumerable<Race> GetAllRaces(bool trackChanges);
        Race GetRace(Guid raceId, bool trackChanges);
        void CreateRace(Race race);
    }
}
