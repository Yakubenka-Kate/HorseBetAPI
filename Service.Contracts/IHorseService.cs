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
        HorseDto CreateHorse(HorseForCreationDto horse);
        IEnumerable<HorseDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        (IEnumerable<HorseDto> horses, string ids) CreateHorsesCollection(IEnumerable<HorseForCreationDto> horseCollection);
        void DeleteHorse(Guid horseId, bool trackChanges);
    }
    
}
