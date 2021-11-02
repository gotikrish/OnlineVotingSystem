using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public class SqlCandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext context;
        public SqlCandidateRepository(AppDbContext context)
        {
            this.context = context;
        }
        Candidate ICandidateRepository.AddCandidate(Candidate candidate)
        {
            context.Candidate.Add(candidate);
            context.SaveChanges();
            return candidate;
        }

        Candidate ICandidateRepository.DeleteCandidate(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Candidate> ICandidateRepository.GetAllCandidate()
        {
            throw new NotImplementedException();
        }

        Candidate ICandidateRepository.GetCandidate(int id)
        {
            throw new NotImplementedException();
        }

        Candidate ICandidateRepository.UpdateCandidate(Candidate candidate)
        {
            throw new NotImplementedException();
        }
    }
}
