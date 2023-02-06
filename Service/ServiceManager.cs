using AutoMapper;
using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IHorseService> _horseService;
        private readonly Lazy<IRaceService> _raceService;
        private readonly Lazy<IEntryService> _entryService;
        private readonly Lazy<IBetService> _betService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _horseService = new Lazy<IHorseService>(() => new HorseService(repositoryManager, logger, mapper));
            _raceService = new Lazy<IRaceService>(() => new RaceService(repositoryManager, logger, mapper));
            _entryService = new Lazy<IEntryService>(() => new EntryService(repositoryManager, logger, mapper));
            _betService = new Lazy<IBetService>(() => new BetService(repositoryManager, logger, mapper));
        }

        public IHorseService HorseService => _horseService.Value;
        public IRaceService RaceService => _raceService.Value;
        public IEntryService EntryService => _entryService.Value;
        public IBetService BetService => _betService.Value;
    }
}
