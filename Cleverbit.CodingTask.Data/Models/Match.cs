using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cleverbit.CodingTask.Data.Models
{
    public class Match : BaseEntity
    {
        public string MatchName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        virtual public ICollection<MatchParticipant> MatchParticipant { get; set; }
    }
}
