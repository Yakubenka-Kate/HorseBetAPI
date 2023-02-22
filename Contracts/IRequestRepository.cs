using Entities.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRequestRepository
    {
       double GetAllRatesForRaceId(Guid raceId);
        double GetAllWinRatesForRaceId(Guid raceId);
    }
}
