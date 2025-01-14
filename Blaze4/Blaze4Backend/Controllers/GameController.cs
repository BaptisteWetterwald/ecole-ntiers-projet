using Blaze4.Application.Models;
using Blaze4.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blaze4Backend.Controllers
{
    
    // GameController: manages endpoints for game-related operations
    // POST api/game/start, POST api/game/play-turn
    
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("start")]
        public IActionResult StartGame(string hostLogin)
        {
            _gameService.StartGame(hostLogin);
            return Ok();
        }

        [HttpPost("play-turn")]
        public IActionResult PlayTurn(string playerLogin, int column)
        {
            _gameService.PlayTurn(playerLogin, column);
            return Ok();
        }
    }
    
}