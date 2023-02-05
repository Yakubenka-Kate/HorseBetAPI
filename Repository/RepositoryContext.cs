using HorseBet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository 
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Horse>? Horses { get; set; }
        public DbSet<Race>? Races { get; set; }
        public DbSet<Entry>? Entries { get; set; }
        public DbSet<Bet>? Bets { get; set; }

    }
}
