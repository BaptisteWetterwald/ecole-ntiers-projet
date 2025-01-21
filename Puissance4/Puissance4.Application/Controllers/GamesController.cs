using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puissance4.Application.DTOs;
using Puissance4.Application.Mappers;
using Puissance4.Application.Services;

namespace Puissance4.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class GamesController : ControllerBase
{
    private readonly GameService _gameService;
    //private readonly AuthService _authService;

    public GamesController(GameService gameService/*, AuthService authService*/)
    {
        _gameService = gameService;
        //_authService = authService;
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GameDto>> GetGameById(int id)
    {
        var game = await _gameService.GetGameById(id);

        if (game == null)
        {
            return NotFound();
        }

        var gameDto = GameMapper.ToDto(game);
        return Ok(gameDto);
    }
    

    [HttpPost("create")]
    public IActionResult CreateGame()
    {
        var game = _gameService.CreateGame();
        return Ok(game);
    }
    
    [HttpPost("{id:int}/join")]
    public IActionResult JoinGame(int id, int guestId)
    {
        var game = _gameService.JoinGame(id, guestId);
        return Ok(game);
    }

    [HttpPost("{gameId:int}/play")]
    public IActionResult PlayTurn(int gameId, int column)
    {
        /* Implémenter les repos avant de décommenter
        var playerId = _authService.GetUserId();
        if (string.IsNullOrEmpty(playerId.ToString()))
            return Unauthorized("Player not authenticated.");
        */
        
        try
        {
            var game = _gameService.PlayTurn(gameId, /*playerId*/0, column); // Appel au service métier
            return Ok(game);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}