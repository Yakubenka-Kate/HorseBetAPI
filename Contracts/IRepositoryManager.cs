using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IHorseRepository Horse{ get; }
        IRaceRepository Race{ get; }
        IEntryRepository Entry{ get; }
        IBetRepository Bet{ get; }

        Task SaveAsync();
    }
}
