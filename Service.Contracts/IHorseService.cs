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
    public interface IHorseService
    {
        Task<IEnumerable<HorseDto>> GetAllHorsesAsync(bool trackChanges);
        Task<Horse> GetHorseAsync(Guid horseId, bool trackChanges);
        Task<HorseDto> CreateHorseAsync(HorseManipulationDto horse);
        Task<IEnumerable<HorseDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<(IEnumerable<HorseDto> horses, string ids)> CreateHorsesCollectionAsync(IEnumerable<HorseManipulationDto> horseCollection);
        Task DeleteHorseAsync(Guid horseId, bool trackChanges);
    }
    
}
