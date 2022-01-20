using Cleverbit.CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Data.Repository
{
    public class MatchRepository : GenericRepository<Match>, IMatchRepository
    {
        public MatchRepository(CodingTaskContext dbContext) : base(dbContext)
        {
        }

        public async Task<Match> GetActiveMatch()
        {
            var matches = await GetAll().Where(x => x.StartDate < DateTime.UtcNow &&
           x.ExpiryDate > DateTime.UtcNow).ToListAsync();

            var activeMatch = matches.OrderBy(x => x.ExpiryDate).FirstOrDefault();
            return activeMatch;
        }
        public async Task<IEnumerable<Match>> GetPlayedMatches()
        {
            return await GetAll().Where(x => x.ExpiryDate <= DateTime.UtcNow).
                OrderBy(x => x.ExpiryDate).ToListAsync();
        }
    }
}
