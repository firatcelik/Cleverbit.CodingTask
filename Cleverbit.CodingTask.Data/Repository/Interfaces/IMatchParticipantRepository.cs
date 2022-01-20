using Cleverbit.CodingTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Data.Repository
{
    public interface IMatchParticipantRepository : IRepository<MatchParticipant>
    {
        Task<MatchParticipant> GetLastMatchParticipantsByUserId(int userId);
        Task<MatchParticipant> GetMatchParticipantByUserIdMatchId(int userId, int matchId);
    }
}
