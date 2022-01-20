using System;
using System.Collections.Generic;
using System.Text;

namespace Cleverbit.CodingTask.Data.Models
{
    public class MatchParticipant : BaseEntity
    {
        public int UserId { get; set; }

        public int MatchId { get; set; }

        public int ParticipantScore { get; set; }

        public DateTime RecordDate { get; set; }

        public virtual User Participant { get; set; }
        public virtual Match Match { get; set; }
    }
}
