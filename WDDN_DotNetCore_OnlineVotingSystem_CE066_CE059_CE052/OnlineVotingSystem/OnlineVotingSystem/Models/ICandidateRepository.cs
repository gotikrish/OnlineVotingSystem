using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public interface ICandidateRepository
    {
        Candidate GetCandidate(int id);
        Candidate GetCandidateWithHighestVotes();
        IEnumerable<Candidate> GetAllCandidate();
        Candidate AddCandidate(Candidate candidate);
        void UpdateCandidate(Candidate candidate);
        Candidate DeleteCandidate(int id);
    }
}
