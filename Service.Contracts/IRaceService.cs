using Entities.Models;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ManipulationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IRaceService
    {
        Task<IEnumerable<RaceDto>> GetAllRacesAsync(bool trackChanges);
        Task<Race> GetRaceAsync(Guid raceId, bool trackChanges);
        Task<RaceDto> CreateRaceAsync(RaceManipulationDto race);
    }
}
