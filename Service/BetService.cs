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

        public async Task<BetDto> CreateBetAsync(Guid entryId, BetManipulationDto betForCreation, bool trackChanges)
        {
            var entry = await _repository.Entry.GetEntryByIdAsync(entryId, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(entryId);

            var betEntity = _mapper.Map<Bet>(betForCreation);

            _repository.Bet.CreateBet(entryId, betEntity);
            await _repository.SaveAsync();

            var betToReturn = _mapper.Map<BetDto>(betEntity);

            return betToReturn;
        }

        public async Task DeleteBetForEntryAsync(Guid entryId, Guid id, bool trackChanges)
        {
            var entry = await _repository.Entry.GetEntryByIdAsync(entryId, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(entryId);

            var betForEntry = await _repository.Bet.GetBetForEntryAsunc(entryId, id, trackChanges);

            if (betForEntry is null)
                throw new BetNotFoundException(id);

            _repository.Bet.DeleteBet(betForEntry);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<Bet>> GetAllBetsAsync(bool trackChanges)
        {
            var bets = await _repository.Bet.GetAllBetsAsunc(trackChanges);

            return bets;
        }

        public async Task<Bet> GetBetForEntryAsync(Guid entryId, Guid id, bool trackChanges)
        {
            var entry = await _repository.Entry.GetEntryByIdAsync(entryId, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(entryId);

            var bet = await _repository.Bet.GetBetForEntryAsunc(entryId, id, trackChanges);

            if (bet is null)
                throw new BetNotFoundException(id);

            return bet;
        }

        public async Task<IEnumerable<Bet>> GetBetsForEntryAsync(Guid entryId, bool trackChanges)
        {
            var entry = await _repository.Entry.GetEntryByIdAsync(entryId, trackChanges);

            if (entry is null)
                throw new EntryNotFoundException(entryId);

            var bets = await _repository.Bet.GetBetsForEntryAsunc(entryId, trackChanges);

            return bets;
        }

        public async Task UpdateBetForEntryAsync(Guid entryId, Guid id, BetManipulationDto betForUpdate, bool entryTrackChanges, bool betTrackChanges)
        {
            var entry = await _repository.Entry.GetEntryByIdAsync(entryId, entryTrackChanges);

            if (entry is null)
                throw new EntryNotFoundException(entryId);

            var betEntity = await _repository.Bet.GetBetForEntryAsunc(entryId, id, betTrackChanges);

            if (betEntity is null)
                throw new BetNotFoundException(id);

            _mapper.Map(betForUpdate, betEntity);
            await _repository.SaveAsync();

        }
    }
}
