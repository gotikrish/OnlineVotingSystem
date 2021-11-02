using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public interface IElectionRepository
    {
        Election GetElection(int id);
        IEnumerable<Election> GetAllElection();
        Election AddElection(Election election);
        Election UpdateElection(Election election);
        Election DeleteElection(int id);
    }
}
