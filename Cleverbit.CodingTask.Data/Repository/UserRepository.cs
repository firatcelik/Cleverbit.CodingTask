using Cleverbit.CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CodingTaskContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());
        }
    }
}
