using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public class SqlResultRepository : IResultRepository
    {
        private readonly AppDbContext context;

        public SqlResultRepository(AppDbContext context)
        {
            this.context = context;
        }
        Result IResultRepository.AddElection(Result result)
        {
            context.Results.Add(result);
            context.SaveChanges();
            return result;
        }

        Result IResultRepository.DeleteCandidate(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Result> IResultRepository.GetAllElectionResult()
        {
            return context.Results;
        }

        Result IResultRepository.GetResultBasedOnElectionEndDate(string endDate)
        {
            Result result = (Result)context.Results.Where(r => r.EndDate == endDate).FirstOrDefault();
            return result;
        }

        void IResultRepository.UpdateResult(Result result)
        {
            context.Results.Update(result);
            context.SaveChanges();
        }
    }
}
