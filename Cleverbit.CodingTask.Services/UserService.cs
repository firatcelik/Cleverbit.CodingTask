using AutoMapper;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Data.Dto;
using Cleverbit.CodingTask.Data.Repository;
using Cleverbit.CodingTask.Services.Extensions;
using Cleverbit.CodingTask.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Services
{
    public class UserService : IUserService
    {
        private readonly IHashService hashService;
        private readonly CodingTaskContext context;
        private readonly IUserRepository userRepository;

        public UserService(IHashService hashService, CodingTaskContext context,IUserRepository _userRepository )
        {
            this.hashService = hashService;
            this.userRepository = _userRepository;
            this.context = context;
        }

        public async Task<AuthResponse> Authenticate(string userName, string password)
        {
            
            var userEntity = await userRepository.GetUserByUserName(userName);
            //var user = await Task.Run(() => context.Users.SingleOrDefault(x => x.UserName == userName && x.Password == givenPassword));

            //TODO: Kullanici bilgisini repo'dan çek yoksa kullanici yok hatasi yoksa şifre hatalı fırlat.
            // return null if user not found
            if (userEntity == null)
            {
               return new AuthResponse()
                {
                    IsAuthenticated = false,
                    AuthErrorMessage= Constants.InCorrectUserNameOrPasswordErrorMessage
               };

            }

            var hashPassword =await hashService.HashText(password);
            if (hashPassword != userEntity.Password)
            {
                return new AuthResponse()
                {
                    IsAuthenticated = false,
                    AuthErrorMessage = Constants.InCorrectPasswordErrorMessage
                };
            }

            // authentication successful so return user details without password
            var credential = $"{userName}:{password}";

            //TODO : JsonIgnore eklenecek.  
            UserDto userDto = Mapper.Map<UserDto>(userEntity);
            userDto.Token = UserServiceExtention.Base64Encode(credential);
            AuthResponse authResponse = new AuthResponse()
            {
                IsAuthenticated = true,
                UserData = userDto
            };

            return authResponse;
        }
    }
}