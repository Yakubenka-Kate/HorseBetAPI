using AutoMapper;
using Contracts;
using Entities.Exceptions;
using HorseBet.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class BetService : IBetService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public BetService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public BetDto CreateBet(Guid entryId, BetForManipulationsDto betForCreation, bool trackChanges)
        {
            var entry = _repository.Entry.GetEntryById(entryId, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(entryId);

            var betEntity = _mapper.Map<Bet>(betForCreation);

            _repository.Bet.CreateBet(entryId, betEntity);
            _repository.Save();

            var betToReturn = _mapper.Map<BetDto>(betEntity);

            return betToReturn;
        }

        public void DeleteBetForEntry(Guid entryId, Guid id, bool trackChanges)
        {
            var entry = _repository.Entry.GetEntryById(entryId, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(entryId);

            var betForEntry = _repository.Bet.GetBetForEntry(entryId, id, trackChanges);

            if (betForEntry is null)
                throw new BetNotFoundException(id);

            _repository.Bet.DeleteBet(betForEntry);
            _repository.Save();
        }

        public IEnumerable<Bet> GetAllBets(bool trackChanges)
        {
            var bets = _repository.Bet.GetAllBets(trackChanges);

            return bets;
        }

        public Bet GetBetForEntry(Guid entryId, Guid id, bool trackChanges)
        {
            var entry = _repository.Entry.GetEntryById(entryId, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(entryId);

            var bet = _repository.Bet.GetBetForEntry(entryId, id, trackChanges);

            if (bet is null)
                throw new BetNotFoundException(id);

            return bet;
        }

        public IEnumerable<Bet> GetBetsForEntry(Guid entryId, bool trackChanges)
        {
            var entry = _repository.Entry.GetEntryById(entryId, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(entryId);

            var bets = _repository.Bet.GetBetsForEntry(entryId, trackChanges);

            return bets;
        }

        public void UpdateBetForEntry(Guid entryId, Guid id, BetForManipulationsDto betForUpdate, bool entryTrackChanges, bool betTrackChanges)
        {
            var entry = _repository.Entry.GetEntryById(entryId, entryTrackChanges);

            if (entry is null)
                throw new EntryNotFoundException(entryId);

            var betEntity = _repository.Bet.GetBetForEntry(entryId, id, betTrackChanges);

            if (betEntity is null)
                throw new BetNotFoundException(id);

            _mapper.Map(betForUpdate, betEntity);
            _repository.Save();

        }
    }
}
