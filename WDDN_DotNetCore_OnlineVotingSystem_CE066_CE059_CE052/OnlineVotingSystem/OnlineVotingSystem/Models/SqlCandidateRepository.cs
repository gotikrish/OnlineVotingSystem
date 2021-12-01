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
            return context.Candidate;
        }

        Candidate ICandidateRepository.GetCandidate(int id)
        {
            return context.Candidate.Where(c => c.Id == id).FirstOrDefault<Candidate>();
        }

        Candidate ICandidateRepository.GetCandidateWithHighestVotes()
        {
            Candidate candidate = (Candidate)context.Candidate.Where(c => c.Votes == (context.Candidate.Max(c => c.Votes))).FirstOrDefault();
            return candidate;
        }

        void ICandidateRepository.UpdateCandidate(Candidate candidate)
        {
            context.Candidate.Update(candidate);
            context.SaveChanges();
        }

        void ICandidateRepository.DeleteAllCandidate()
        {
            context.Candidate.RemoveRange(context.Candidate);
            context.SaveChanges();
        }
    }
}
