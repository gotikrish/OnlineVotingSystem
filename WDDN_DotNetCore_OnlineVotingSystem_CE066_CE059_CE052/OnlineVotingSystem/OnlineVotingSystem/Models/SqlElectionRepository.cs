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
            throw new NotImplementedException();
        }

        Election IElectionRepository.GetElection(int id)
        {
            throw new NotImplementedException();
        }

        Election IElectionRepository.UpdateElection(Election election)
        {
            throw new NotImplementedException();
        }
    }
}
