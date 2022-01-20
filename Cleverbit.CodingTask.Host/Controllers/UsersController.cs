using Cleverbit.CodingTask.Data.Dto;
using Cleverbit.CodingTask.Data.Extentions.ValidationExtensions;
using Cleverbit.CodingTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UsersController> logger;

        public UsersController(IUserService _userService, ILogger<UsersController> _logger)
        {
            this.userService = _userService;
            this.logger = _logger;
        }

        [HttpPost]
        [Route("Authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate(UserDto userInfo)
        {
            //Validate input data with FluentValidation
            UserDtoValidator validation = new UserDtoValidator();
            var validationResult = await validation.ValidateAsync(userInfo);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString());
            }

            var result = await userService.Authenticate(userInfo.UserName, userInfo.Password);
            if (result.IsAuthenticated)
            {
                return this.Ok(result);
            }
            else
            {
                this.logger.LogError($"{userInfo.UserName} is Unauthorized");
                return this.Unauthorized(result);
            }
        }
    }
}
