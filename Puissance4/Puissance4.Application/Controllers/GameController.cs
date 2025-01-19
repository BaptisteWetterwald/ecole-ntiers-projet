using Microsoft.AspNetCore.Mvc;
using Puissance4.Application.Services;

namespace Puissance4.Application.Controllers;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private readonly CellService _cellService;

    public GameController(CellService cellService)
    {
        _cellService = cellService;
    }

    [HttpGet("{id}")] // api/games/1
    public IActionResult GetGameById(int id)
    {
        try
        {
            var token = _cellService.GetCellById(1);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet] // api/games
    public IActionResult Get(int id)
    {
        try
        {
            var token = _cellService.GetCellById(1);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

}