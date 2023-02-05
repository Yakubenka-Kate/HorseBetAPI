using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IHorseService HorseService { get; }
        IRaceService RaceService { get; }
        IEntryService EntryService { get; }
        IBetService BetService { get; }
    }
}
