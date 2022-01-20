using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Cleverbit.CodingTask.Data.Dto
{
   public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
