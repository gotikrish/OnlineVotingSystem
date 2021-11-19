using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public class SqlVoterRepository : IVoterRepository
    {
        private AppDbContext dbContext;

        public SqlVoterRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void ADD(Voter v)
        {
            dbContext.Voter.Add(v);
            dbContext.SaveChanges();
        }

        public void DeleteAll()
        {
            dbContext.Voter.RemoveRange(dbContext.Voter);
            dbContext.SaveChanges();
        }

        public Voter GetVoter(int id)
        {
            return dbContext.Voter.Where(v => v.VoterId == id).FirstOrDefault<Voter>();
        }

        public void UPDATE(Voter v)
        {
            dbContext.Voter.Update(v);
            dbContext.SaveChanges();
        }
    }
}
