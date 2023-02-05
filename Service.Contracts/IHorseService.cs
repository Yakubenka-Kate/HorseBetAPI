using HorseBet.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IHorseService
    {
        IEnumerable<HorseDto> GetAllHorses(bool trackChanges);
        Horse GetHorse(Guid horseId, bool trackChanges);
        HorseDto CreateHorse(HorseForCreation horse);
    }
}
