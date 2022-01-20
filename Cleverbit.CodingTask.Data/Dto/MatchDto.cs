using System;
using System.Collections.Generic;
using System.Text;

namespace Cleverbit.CodingTask.Data.Dto
{
    public class MatchDto
    {
        public int Id { get; set; }
        public string MatchName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? WinnerMatchScore { get; set; }
        public string Winner { get; set; }

    }
}
