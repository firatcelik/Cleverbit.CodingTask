using System;
using System.Collections.Generic;
using System.Text;

namespace Cleverbit.CodingTask.Data.Dto
{
    public class MatchParticipantDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int MatchId { get; set; }
        public string MatchName { get; set; }
        public int? ParticipantScore { get; set; }
    }
}
