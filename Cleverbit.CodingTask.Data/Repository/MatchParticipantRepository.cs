using Cleverbit.CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Data.Repository
{
    public class MatchParticipantRepository : GenericRepository<MatchParticipant>, IMatchParticipantRepository
    {

        public MatchParticipantRepository(CodingTaskContext dbContext) : base(dbContext)
        {
        }

        public async Task<MatchParticipant> GetLastMatchParticipantsByUserId(int userId)
        {
           return await GetAll().Where(x => x.UserId == userId).
                OrderByDescending(x => x.RecordDate).FirstOrDefaultAsync();
        }

        public async Task<MatchParticipant> GetMatchParticipantByUserIdMatchId(int userId, int matchId)
        {
            var match = await GetAll().Where(x => x.MatchId == matchId && x.UserId == userId).FirstOrDefaultAsync();

            return match;
        }
    }
}
