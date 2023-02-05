using Contracts;
using HorseBet.Models;
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

        public IEnumerable<Entry> GetAllEntries(bool trackChanges) => 
            FindAll(trackChanges).ToList();

        public IEnumerable<Entry> GetEntriesForHorse(Guid horseId, bool trackChanges) => 
            FindByCondition(e => e.HorseId.Equals(horseId), trackChanges).ToList();

        public Entry GetEntry(Guid horseId, Guid id, bool trackChanges) => 
            FindByCondition(e => e.HorseId.Equals(horseId) && e.Id.Equals(id), trackChanges).SingleOrDefault();
    }
}
