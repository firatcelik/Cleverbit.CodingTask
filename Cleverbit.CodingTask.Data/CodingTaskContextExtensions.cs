using Cleverbit.CodingTask.Data.Models;
using Cleverbit.CodingTask.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Data
{
    public static class CodingTaskContextExtensions
    {
        public static async Task Initialize(this CodingTaskContext context, IHashService hashService)
        {
            await context.Database.EnsureCreatedAsync();

            var currentUsers = await context.Users.ToListAsync();

            bool anyNewUser = false;

            if (!currentUsers.Any(u => u.UserName == "User1"))
            {
                context.Users.Add(new Models.User
                {
                    UserName = "User1",
                    Password = await hashService.HashText("Password1")
                });

                anyNewUser = true;
            }

            if (!currentUsers.Any(u => u.UserName == "User2"))
            {
                context.Users.Add(new Models.User
                {
                    UserName = "User2",
                    Password = await hashService.HashText("Password2")
                });

                anyNewUser = true;
            }

            if (!currentUsers.Any(u => u.UserName == "User3"))
            {
                context.Users.Add(new Models.User
                {
                    UserName = "User3",
                    Password = await hashService.HashText("Password3")
                });

                anyNewUser = true;
            }

            if (!currentUsers.Any(u => u.UserName == "User4"))
            {
                context.Users.Add(new Models.User
                {
                    UserName = "User4",
                    Password = await hashService.HashText("Password4")
                });

                anyNewUser = true;
            }

            if (anyNewUser)
            {
                await context.SaveChangesAsync();
            }

            await SeedGameMatches(context);
        }

        private static async Task SeedGameMatches(this CodingTaskContext ctx)
        {
            var activeMatch = ctx.Matches.Where(x => x.StartDate < DateTime.UtcNow &&
             x.ExpiryDate > DateTime.UtcNow);

            if (activeMatch.Count() < 5)
            {
                DateTime startTime = DateTime.UtcNow;
                var newMatchList = new List<Match>();
                for (int i = 0; i < 5; i++)
                {
                    newMatchList.Add(new Match()
                    {
                        MatchName = "Match--" + Guid.NewGuid(),
                        ExpiryDate = startTime.AddHours(1),
                        StartDate = startTime

                    });
                }

                ctx.Matches.AddRange(newMatchList);

                await ctx.SaveChangesAsync();
            }
        }
    }
}
