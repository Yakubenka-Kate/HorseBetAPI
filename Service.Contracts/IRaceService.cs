using HorseBet.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IRaceService
    {
        IEnumerable<RaceDto> GetAllRaces(bool trackChanges);
        Race GetRace(Guid raceId, bool trackChanges);
        RaceDto CreateRace(RaceForCreationDto race);
    }
}
