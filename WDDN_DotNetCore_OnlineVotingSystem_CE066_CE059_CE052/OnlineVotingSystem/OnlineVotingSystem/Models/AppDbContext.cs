using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Election> Election { get; set; }
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<Voter> Voter { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
