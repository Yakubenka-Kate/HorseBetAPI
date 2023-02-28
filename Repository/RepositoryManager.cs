using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IHorseRepository> _horseRepository;
        private readonly Lazy<IRaceRepository> _raceRepository;
        private readonly Lazy<IEntryRepository> _entryRepository;
        private readonly Lazy<IBetRepository> _betRepository;
        private readonly Lazy<IRequestRepository> _requestRepository;
        private readonly Lazy<IUserRepository> _userRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _horseRepository = new Lazy<IHorseRepository>(() => new HorseRepository(repositoryContext));
            _raceRepository = new Lazy<IRaceRepository>(() => new RaceRepository(repositoryContext));
            _entryRepository = new Lazy<IEntryRepository>(() => new EntryRepository(repositoryContext));
            _betRepository = new Lazy<IBetRepository>(() => new BetRepository(repositoryContext));       
            _requestRepository = new Lazy<IRequestRepository>(() => new RequestRepository(repositoryContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
        }

        public IHorseRepository Horse => _horseRepository.Value;

        public IRaceRepository Race => _raceRepository.Value;

        public IEntryRepository Entry => _entryRepository.Value;

        public IBetRepository Bet => _betRepository.Value;

        public IRequestRepository Request => _requestRepository.Value;

        public IUserRepository User => _userRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
