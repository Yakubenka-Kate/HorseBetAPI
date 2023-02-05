using Contracts;
using HorseBet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BetRepository : RepositoryBase<Bet>, IBetRepository
    {
        public BetRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
