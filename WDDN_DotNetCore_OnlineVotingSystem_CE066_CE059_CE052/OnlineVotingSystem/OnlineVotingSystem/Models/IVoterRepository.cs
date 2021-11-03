using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public interface IVoterRepository
    {
        public Voter GetVoter(int id);

        public void ADD(Voter v);

        public void UPDATE(Voter v);
    }
}
