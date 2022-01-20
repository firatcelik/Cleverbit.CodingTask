using AutoMapper;
using Cleverbit.CodingTask.Data.Dto;
using Cleverbit.CodingTask.Data.Models;
using Cleverbit.CodingTask.Data.Repository;
using Cleverbit.CodingTask.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Services
{
    public class GameService : IGameService
    {
        private readonly IUserRepository userRepository;
        private readonly IMatchRepository matchRepository;
        private readonly IMatchParticipantRepository matchParticipantRepository;

        public GameService(IMatchRepository _matchRepository, IMatchParticipantRepository _matchParticipantRepository, IUserRepository _userRepository)
        {
            matchRepository = _matchRepository;
            matchParticipantRepository = _matchParticipantRepository;
            userRepository = _userRepository;
        }

        public async Task<MatchDto> GetActiveMatch()
        {
            var activeMatch = await matchRepository.GetActiveMatch();
            if (activeMatch == null)
            {
                throw new NullReferenceException("No active match found.");
            }

            return Mapper.Map<MatchDto>(activeMatch);
        }

        public async Task<MatchDto> GetUserActiveMatch(int userId)
        {
            var userLastMatchParticipant = await matchParticipantRepository.GetLastMatchParticipantsByUserId(userId);

            var activeMatch = await matchRepository.GetActiveMatch();

            if (userLastMatchParticipant == null || activeMatch == null || userLastMatchParticipant.MatchId != activeMatch.Id)
            {
                throw new NullReferenceException("No active match found.");
            }

            return Mapper.Map<MatchDto>(activeMatch);
        }

        public async Task<List<MatchDto>> GetPlayedMatches()
        {
            var expiredMatchDtoList = new List<MatchDto>();

            var expiredMatches = await this.matchRepository.GetPlayedMatches();

            //Find macht winner
            foreach (var match in expiredMatches)
            {
                var matchWinners = await this.getMatchWinner(match.Id);
                var item = new MatchDto()
                {
                    Id = match.Id,
                    MatchName = match.MatchName,
                    ExpiryDate = match.ExpiryDate,
                    StartDate = match.StartDate,
                    WinnerMatchScore = matchWinners == null || matchWinners.Count() == 0 ? -1 : matchWinners.FirstOrDefault().ParticipantScore,
                    Winner = matchWinners == null || matchWinners.Count() == 0 ? "no winner" : string.Join(",", matchWinners.Select(x => x.UserName))
                };
                expiredMatchDtoList.Add(item);
            }

            return expiredMatchDtoList;
        }
        public async Task<MatchParticipantDto> JoinMatch(int userId)
        {
            var activeMatch = await GetActiveMatch();

            await checkUserPlayedActiveMatch(userId, activeMatch);

            //Choose random number for match
            var randomNumber = new Random(Guid.NewGuid().GetHashCode()).Next(1, 100);

            var newMatch = new MatchParticipant()
            {
                UserId = userId,
                MatchId = activeMatch.Id,
                ParticipantScore = randomNumber,
                RecordDate = DateTime.UtcNow
            };

            await this.matchParticipantRepository.AddAsync(newMatch);

            var userMatchInfo = new MatchParticipantDto()
            {
                MatchId = activeMatch.Id,
                MatchName = activeMatch.MatchName,
                UserId = userId,
                ParticipantScore = randomNumber
            };

            return userMatchInfo;
        }

        private async Task<List<MatchParticipantDto>> getMatchWinner(int matchId)
        {
            var matchParticipants = await this.matchParticipantRepository.Find(x => x.MatchId == matchId);
            if (matchParticipants == null || matchParticipants.Count() <= 0)
            {
                return null;
            }

            int maxScore = matchParticipants.Max(x => x.ParticipantScore);

            var matchWinner = matchParticipants.Where(item => item.ParticipantScore == maxScore).ToList();


            return Mapper.Map<List<MatchParticipantDto>>(matchWinner);
        }
        private async Task checkUserPlayedActiveMatch(int userId, MatchDto activeMatch)
        {
            var userActiveMatch = await matchParticipantRepository.GetMatchParticipantByUserIdMatchId(userId, activeMatch.Id);

            if (userActiveMatch != null)
            {
                throw new NullReferenceException("User already played match!");
            }
        }
    }
}