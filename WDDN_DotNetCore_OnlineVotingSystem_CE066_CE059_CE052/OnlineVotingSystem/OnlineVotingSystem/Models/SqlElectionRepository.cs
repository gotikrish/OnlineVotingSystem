using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public class SqlElectionRepository : IElectionRepository
    {
        private readonly AppDbContext context;
        public SqlElectionRepository(AppDbContext dbContext)
        {
            this.context = dbContext;
        }

        public void DeleteAllElection()
        {
            context.Election.RemoveRange(context.Election);
            context.SaveChanges();
        }

        Election IElectionRepository.AddElection(Election election)
        {
            context.Election.Add(election);
            context.SaveChanges();
            return election;
        }

        Election IElectionRepository.DeleteElection(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Election> IElectionRepository.GetAllElection()
        {
            return context.Election;
        }

        Election IElectionRepository.UpdateElection(Election election)
        {
            throw new NotImplementedException();
        }
    }
}
