using System;
using System.Collections.Generic;
using System.Text;

namespace Cleverbit.CodingTask.Data.Models
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
