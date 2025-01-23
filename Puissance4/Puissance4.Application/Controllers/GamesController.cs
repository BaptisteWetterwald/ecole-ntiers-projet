using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puissance4.DTOs;
using Puissance4.Application.Services;

namespace Puissance4.Application.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly GameService _gameService;
    private readonly AuthService _authService;

    public GamesController(GameService gameService, AuthService authService)
    {
        _gameService = gameService;
        _authService = authService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GameDto>> GetGameById(int id)
    {
        var gameDto = await _gameService.GetGameById(id);

        if (gameDto == null)
        {
            return NotFound();
        }
        
        return Ok(gameDto);
    }
    
    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetPendingGames()
    {
        var games = await _gameService.GetPendingGames();
        return Ok(games);
    }
    
    [HttpGet("player")]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesOfPlayer()
    {
        var playerId = _authService.GetUserId();
        var games = await _gameService.GetGamesOfPlayer(playerId);
        return Ok(games);
    }

    [HttpPost("create")]
    public async Task<ActionResult<GameDto>> CreateGame()
    {
        var playerId = _authService.GetUserId();
        var gameDto = await _gameService.CreateGame(playerId);
        return Ok(gameDto);
    }
    
    [HttpPost("{gameId:int}/join")]
    public async Task<ActionResult<GameDto>> JoinGame(int gameId)
    {
        try
        {
            var playerId = _authService.GetUserId();
            var gameDto = await _gameService.JoinGame(gameId, playerId);
            return Ok(gameDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("{gameId:int}/play")]
    public async Task<ActionResult<GameDto>> PlayTurn(int gameId, [FromBody] PlayTurnDto playTurnDto)
    {
        try
        {
            var playerId = _authService.GetUserId();
            var gameDto = await _gameService.PlayTurn(gameId, playerId, playTurnDto.Column);
            return Ok(gameDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}