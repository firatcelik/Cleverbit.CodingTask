using Cleverbit.CodingTask.Data.Dto;
using Cleverbit.CodingTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Services
{
    public interface IUserService
    {
        Task<AuthResponse> Authenticate(string userName, string password);
    }
}
