using Microsoft.AspNetCore.Mvc;
using Puissance4.Application.Domain;
using Puissance4.Application.Services;

[ApiController]
[Route("api/tokens")]
public class TokenController : ControllerBase
{
    private readonly TokenService _tokenService;

    public TokenController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpGet("{id}")] // api/tokens/1
    public IActionResult GetTokenById(int id)
    {
        try
        {
            var token = _tokenService.GetTokenById(id);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult AddToken([FromBody] Token token)
    {
        _tokenService.AddToken(token);
        return Ok();
    }
}