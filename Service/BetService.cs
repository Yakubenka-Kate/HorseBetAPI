using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ManipulationDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class BetService : IBetService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public BetService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<BetDto> CreateBetAsync(Guid entryId, BetManipulationDto betForCreation, bool trackChanges)
        {
            var entry = await _repository.Entry.GetEntryByIdAsync(entryId, trackChanges);

            if (entry is null)
                throw new NotFoundException($"The entry with id: {entryId} doesn't exist in the database!");

            if (entry.Result != 0)
                throw new BadRequestException("The entry has already taken place");

            var betEntity = _mapper.Map<Bet>(betForCreation);

            var user = await _userManager.FindByIdAsync(betForCreation.UserId);
            if (user is null)
                throw new NotFoundException($"The use with id: {betForCreation.UserId} doesn't exist in the database!");
            
            var userBets = await _repository.Bet.GetAllBetsForUserAsync(betForCreation.UserId, trackChanges);
            foreach(var userBet in userBets)
            {
                var e = await _repository.Entry.GetEntryByIdAsync(userBet.EntryId, trackChanges);

                if (userBet.EntryId == entryId || entry.RaceId == e.RaceId)
                    throw new BadRequestException("The bet on this race has already been made by this user");
            }

            if (betEntity.Rate > user.Balance)
                throw new BadRequestException("Not enough money");

            var race = await _repository.Race.GetRaceAsync(entry.RaceId, trackChanges);

            if (betEntity.BetPosition > race.CountHorses || betEntity.BetPosition < 0)
                throw new BadRequestException("Incorrect position");

            user.Balance -= betEntity.Rate;
            await _userManager.UpdateAsync(user);

            _repository.Bet.CreateBet(entryId, betEntity);
            await _repository.SaveAsync();

            await UpdateCoefForBet(entry.RaceId, trackChanges: true);

            var betToReturn = _mapper.Map<BetDto>(betEntity);

            return betToReturn;
        }

        private async Task UpdateCoefForBet(Guid raceId, bool trackChanges)
        {
            var entriesForRace = await _repository.Entry.GetEntriesForRaceAsync(raceId, trackChanges);
            var allMoney =  _repository.Request.GetAllRatesForRaceId(raceId);

            foreach (var entryForRace in entriesForRace)
            {
                var entry = await _repository.Entry.GetEntryByIdAsync(entryForRace.Id, trackChanges);
                var bets = await _repository.Bet.GetBetsForEntryAsunc(entry.Id, trackChanges);
               
                var money = await _repository.Bet.GetRatesForEntryAsync(bets, trackChanges);
                if (money == 0)
                    entry.Coefficient = 0;
                else 
                    entry.Coefficient = allMoney / money;

                _repository.Entry.UpdateEntry(entry);
                await _repository.SaveAsync();
            }
        }

        public async Task DeleteBetForEntryAsync(Guid entryId, Guid id, bool trackChanges)
        {
            var entry = await _repository.Entry.GetEntryByIdAsync(entryId, trackChanges);

            if (entry is null)
                throw new NotFoundException($"The entry with id: {entryId} doesn't exist in the database!");

            var betForEntry = await _repository.Bet.GetBetForEntryAsunc(entryId, id, trackChanges);

            if (betForEntry is null)
                throw new NotFoundException($"The bet with id: {id} doesn't exist in the database!");

            _repository.Bet.DeleteBet(betForEntry);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<Bet>> GetAllBetsForUserAsync(string userId, bool trackChanges)
        {
            var bets = await _repository.Bet.GetAllBetsForUserAsync(userId, trackChanges);

            return bets;
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
                throw new NotFoundException($"The entry with id: {entryId} doesn't exist in the database!");

            var bet = await _repository.Bet.GetBetForEntryAsunc(entryId, id, trackChanges);

            if (bet is null)
                throw new NotFoundException($"The bet with id: {id} doesn't exist in the database!");

            return bet;
        }

        public async Task<IEnumerable<Bet>> GetBetsForEntryAsync(Guid entryId, bool trackChanges)
        {
            var entry = await _repository.Entry.GetEntryByIdAsync(entryId, trackChanges);

            if (entry is null)
                throw new NotFoundException($"The entry with id: {entryId} doesn't exist in the database!");

            var bets = await _repository.Bet.GetBetsForEntryAsunc(entryId, trackChanges);

            return bets;
        }

        public async Task UpdateBetForEntryAsync(Guid entryId, Guid id, BetManipulationDto betForUpdate, bool entryTrackChanges, bool betTrackChanges)
        {
            var entry = await _repository.Entry.GetEntryByIdAsync(entryId, entryTrackChanges);

            if (entry is null)
                throw new NotFoundException($"The entry with id: {entryId} doesn't exist in the database!");

            var betEntity = await _repository.Bet.GetBetForEntryAsunc(entryId, id, betTrackChanges);

            if (betEntity is null)
                throw new NotFoundException($"The bet with id: {id} doesn't exist in the database!");

            _mapper.Map(betForUpdate, betEntity);
            await _repository.SaveAsync();

        }
    }
}
