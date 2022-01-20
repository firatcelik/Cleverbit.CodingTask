using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Services;
using Cleverbit.CodingTask.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Cleverbit.CodingTask.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameService gameService;
        private readonly ILogger<GameController> logger;

        public GameController(IGameService _gameService, ILogger<GameController> _logger)
        {
            this.gameService = _gameService;
            this.logger = _logger;
        }

        [HttpGet]
        [Route("GetActiveMatch")]
        public async Task<ActionResult> GetActiveMatch()
        {
            try
            {
                var activeMatch = await this.gameService.GetActiveMatch();
                return this.Ok(activeMatch);
            }
            catch (NullReferenceException ex)
            {
                this.logger.LogError(ex.Message, ex);
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPlayedMatches")]
        [AllowAnonymous]
        public async Task<ActionResult> GetPlayedMatches()
        {
            try
            {
                var matches = await this.gameService.GetPlayedMatches();
                return this.Ok(matches);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message, ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("JoinMatch")]
        public async Task<ActionResult> JoinMatch()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var input = await this.gameService.JoinMatch(userId);
                return this.Ok(input);
            }
            catch (NullReferenceException ex)
            {
                this.logger.LogError(ex.Message, ex);
                return this.NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserActiveMatch")]
        public async Task<ActionResult> GetUserActiveMatch()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var activeMatch = await this.gameService.GetUserActiveMatch(userId);
                return this.Ok(activeMatch);
            }
            catch (NullReferenceException ex)
            {
                this.logger.LogError(ex.Message, ex);
                return this.NotFound(ex.Message);
            }
        }
    }
}
