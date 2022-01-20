using AutoMapper;
using AutoMapper.Configuration;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Data.Repository;
using Cleverbit.CodingTask.Services;
using Cleverbit.CodingTask.Services.Interfaces;
using Cleverbit.CodingTask.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cleverbit.CodingTask.Host.DependencyInjections
{
    public static class InjectCodingTaskDependencies
    {
        public static void AddWebSiteDependencies(this IServiceCollection services)
        {
        

            services.AddAutoMapper();

            services.AddTransient<IUserService, UserService>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IMatchParticipantRepository, MatchParticipantRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGameService, GameService>();//<IRandomNumberGameService, RandomNumberGameService>();
        }
    }
}
