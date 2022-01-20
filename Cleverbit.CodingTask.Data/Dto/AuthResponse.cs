using System;
using System.Collections.Generic;
using System.Text;

namespace Cleverbit.CodingTask.Data.Dto
{
    public class AuthResponse
    {
        public bool IsAuthenticated { get; set; }
        public UserDto UserData { get; set; }
        public string AuthErrorMessage { get; set; }
    }
}
