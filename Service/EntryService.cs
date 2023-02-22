using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ManipulationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class EntryService : IEntryService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EntryService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task CreateFullEntry(Guid raceId, IEnumerable<Guid> horseIds, bool trackChanges)
        {
            var race = await _repository.Race.GetRaceAsync(raceId, trackChanges);

            if (race is null)
                throw new NotFoundException($"The race with id: {raceId} doesn't exist in the database!");

            if (horseIds is null)
                throw new BadRequestException("Parameter ids is null");

            var horseEntities = await _repository.Horse.GetByIdsAsync(horseIds, trackChanges);

            if (horseIds.Count() != horseEntities.Count())
                throw new BadRequestException("Collection count mismatch comparing to ids.");

            if (race.CountHorses != horseEntities.Count())
                throw new BadRequestException("The declared number of horses does not match the entered data");

            foreach (var horseEntity in horseEntities)
            {
                _repository.Entry.CreateEntry(raceId, horseEntity.Id, new Entry());
                await _repository.SaveAsync();
            }
        }

        public async Task<EntryDto> CreateEntryAsync(Guid raceId, Guid horseId, EntryManipulationDto entryForCreation, bool trackChanges)
        {
            var horse = await _repository.Horse.GetHorseAsync(horseId, trackChanges);
            
            if (horse is null)
                throw new NotFoundException($"The horse with id: {horseId} doesn't exist in the database!");

            var race = await _repository.Race.GetRaceAsync(raceId, trackChanges);

            if (race is null)
                throw new NotFoundException($"The race with id: {raceId} doesn't exist in the database!");

            var entryEntity = _mapper.Map<Entry>(entryForCreation);

            _repository.Entry.CreateEntry(raceId, horseId, entryEntity);
            await _repository.SaveAsync();

            var raceToReturn = _mapper.Map<EntryDto>(entryEntity);

            return raceToReturn;
        }

        public async Task DeleteEntryForHorseAsync(Guid horseId, Guid id, bool trackChanges)
        {
            var horse = await _repository.Horse.GetHorseAsync(horseId, trackChanges);

            if (horse is null)
                throw new NotFoundException($"The horse with id: {horseId} doesn't exist in the database!");

            var entryForHorse = await _repository.Entry.GetEntryForHorseAsync(horseId, id, trackChanges);

            if (entryForHorse is null)
                throw new NotFoundException($"The entry with id: {id} doesn't exist in the database!");

            _repository.Entry.DeleteEntry(entryForHorse);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<Entry>> GetAllEntriesAsync(bool trackChanges)
        {
            var entries = await _repository.Entry.GetAllEntriesAsync(trackChanges);

            return entries;
        }

        public async Task<IEnumerable<Entry>> GetEntriesForHorseAsync(Guid horseId, bool trackChanges)
        {
            var horse = await _repository.Horse.GetHorseAsync(horseId, trackChanges);

            if (horse is null)
                throw new NotFoundException($"The horse with id: {horseId} doesn't exist in the database!");

            var entries = await _repository.Entry.GetEntriesForHorseAsync(horseId, trackChanges);

            return entries;
        }

        public async Task<Entry> GetEntryByIdAsync(Guid id, bool trackChanges)
        {
            var entry = await _repository.Entry.GetEntryByIdAsync(id, trackChanges);

            if (entry is null)
                throw new NotFoundException($"The entry with id: {id} doesn't exist in the database!");

            return entry;
        }

        public async Task<Entry> GetEntryForHorseAsync(Guid horseId, Guid id, bool trackChanges)
        {
            var horse = await _repository.Horse.GetHorseAsync(horseId, trackChanges);

            if(horse is null)
                throw new NotFoundException($"The horse with id: {horseId} doesn't exist in the database!");

            var entry = await _repository.Entry.GetEntryForHorseAsync(horseId, id, trackChanges);

            if (entry is null)
                throw new NotFoundException($"The entry with id: {id} doesn't exist in the database!");

            return entry;
        }

        public async Task UpdateEntryForHorseAsync(Guid horseId, Guid id, EntryManipulationDto entryForUpdate, 
            bool horseTrackChanges, bool entryTrackChanges)
        {
            var horse = await _repository.Horse.GetHorseAsync(horseId, horseTrackChanges);

            if (horse is null)
                throw new NotFoundException($"The horse with id: {horseId} doesn't exist in the database!");

            var entryEntity = await _repository.Entry.GetEntryForHorseAsync(horseId, id, entryTrackChanges);

            if (entryEntity is null)
                throw new NotFoundException($"The entry with id: {id} doesn't exist in the database!");

            _mapper.Map(entryForUpdate, entryEntity);
            await _repository.SaveAsync();

        }

        public async Task<IEnumerable<Entry>> GetResult(Guid raceId, bool trackChanges)
        {
            var entries = await _repository.Entry.GetEntriesForRaceAsync(raceId, trackChanges);

            if (entries is null)
                throw new NotFoundException($"The race with id: {raceId} doesn't exist in the database!");

            var x = RandomResult(entries);
            int j = 0;
            foreach (var entry in entries)
            {
                entry.Result = x[j];
                j++;
                _repository.Entry.UpdateEntry(entry);
            }

            await _repository.SaveAsync();

            var coef = _repository.Request.GetAllRatesForRaceId(raceId) / _repository.Request.GetAllWinRatesForRaceId(raceId);

            foreach (var entry in entries)
            {
                var bets = await _repository.Bet.GetBetsForEntryAsunc(entry.Id, trackChanges);
                foreach (var bet in bets)
                {
                    if (bet.BetPosition == entry.Result)
                    {
                        bet.Result = "Win";
                        var user = await _repository.User.GetUserAsync(bet.UserId, trackChanges);
                        user.Balance += (bet.Rate * coef);
                        _repository.User.UpdateUser(user);
                    }                      
                    else
                        bet.Result = "Lose";

                    _repository.Bet.UpdateBet(bet);                                  
                    await _repository.SaveAsync();
                }
            }  

            return entries;
        }

        private static int[] RandomResult(IEnumerable<Entry> entries)
        {
            var rnd = new Random();
            var x = new int[entries.Count()];

            for (int i = 0; i < entries.Count(); i++)
            {
                bool contains;
                int next;

                do
                {
                    next = rnd.Next(1, entries.Count() + 1);
                    contains = false;
                    for (int index = 0; index < i; index++)
                    {
                        int n = x[index];
                        if (n == next)
                        {
                            contains = true;
                            break;
                        }
                    }
                } while (contains);
                x[i] = next;
            }
            var y = new int[3] { 1, 3, 2};

            return y;
        }
    }
}
