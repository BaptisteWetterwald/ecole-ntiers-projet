using Blaze4.Application.Models;
using Blaze4.Application.Services;
using Blaze4Backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Blaze4Backend.Controllers;
    
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

    // POST api/game/start : starts a new game with given id and player logins
    [HttpPost("start")]
    public ActionResult<Game> StartGame(string hostLogin)
    {
        var host = new Player { Login = hostLogin };
        var game = _gameService.CreateGame(host);
        return game;
    }
    
    // POST api/game/play-turn : plays a turn in the game with given id
    [HttpPost("play-turn")]
    public ActionResult<string> PlayTurn(Guid gameId, string playerLogin, int column)
    {
        var player = new Player { Login = playerLogin };
        var result = _gameService.PlayTurn(gameId, player, column);
        return result;
    }
    
    [HttpGet("{id}")]
    public IActionResult GetGameById(Guid id)
    {
        /*var game = _gameService.GetGameById(id);
        if (game == null)
        {
            return NotFound("Game not found.");
        }

        return Ok(GameMapper.ToDto(game));*/
        return Ok();
    }
}