using Microsoft.AspNetCore.Mvc;
using Puissance4.Application.Services;

namespace Puissance4.Application.Controllers;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;

    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("create")]
    public IActionResult CreateGame(int hostId)
    {
        var game = _gameService.CreateGame(hostId);
        return Ok(game);
    }
    
    [HttpPost("{id}/join")]
    public IActionResult JoinGame(int id, int guestId)
    {
        var game = _gameService.JoinGame(id, guestId);
        return Ok(game);
    }

    [HttpPost("{id}/play")]
    public IActionResult PlayTurn(int id, int playerId, int column)
    {
        var game = _gameService.PlayTurn(id, playerId, column);
        return Ok(game);
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        var item = _gameService.Test();
        return Ok(item);
    }
    

}