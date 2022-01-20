using Cleverbit.CodingTask.Data.Dto;
using Cleverbit.CodingTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Services.Interfaces
{
    public interface IGameService
    {
        Task<MatchDto> GetActiveMatch();
        Task<List<MatchDto>> GetPlayedMatches();
        Task<MatchParticipantDto> JoinMatch(int userId);
        Task<MatchDto> GetUserActiveMatch(int userId);
    }
}
