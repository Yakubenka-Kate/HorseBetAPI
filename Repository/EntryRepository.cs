using Contracts;
using HorseBet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EntryRepository : RepositoryBase<Entry>, IEntryRepository
    {
        public EntryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEntry(Guid raceId, Guid horseId, Entry entry)
        {
            entry.RaceId = raceId;
            entry.HorseId = horseId;
            Create(entry);
        }

        public void DeleteEntry(Entry entry) => Delete(entry);

        public async Task<IEnumerable<Entry>> GetAllEntriesAsync(bool trackChanges) => 
            await FindAll(trackChanges).ToListAsync();

        public async Task<IEnumerable<Entry>> GetEntriesForHorseAsync(Guid horseId, bool trackChanges) => 
            await FindByCondition(e => e.HorseId.Equals(horseId), trackChanges).ToListAsync();

        public async Task<Entry> GetEntryByIdAsync(Guid id, bool trackChanges) => 
            await FindByCondition(e => e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task<Entry> GetEntryForHorseAsync(Guid horseId, Guid id, bool trackChanges) => 
            await FindByCondition(e => e.HorseId.Equals(horseId) && e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
