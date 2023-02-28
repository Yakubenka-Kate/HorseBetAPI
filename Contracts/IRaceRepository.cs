using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAllRacesAsync(bool trackChanges);
        Task<Race> GetRaceAsync(Guid raceId, bool trackChanges);
        void CreateRace(Race race);
    }
}
