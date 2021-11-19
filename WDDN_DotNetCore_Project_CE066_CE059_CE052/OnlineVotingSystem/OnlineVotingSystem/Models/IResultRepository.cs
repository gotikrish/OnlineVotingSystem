using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public interface IResultRepository
    {
        IEnumerable<Result> GetAllElectionResult();
        Result AddElection(Result result);
        void UpdateResult(Result result);
        Result GetResultBasedOnElectionEndDate(string endDate);
        Result DeleteCandidate(int id);
    }
}
