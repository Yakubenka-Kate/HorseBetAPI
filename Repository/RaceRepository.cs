using Contracts;
using HorseBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RaceRepository : RepositoryBase<Race>, IRaceRepository
    {
        public RaceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
