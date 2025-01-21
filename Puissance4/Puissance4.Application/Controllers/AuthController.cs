/*
using Microsoft.AspNetCore.Mvc;
using Puissance4.Application.DTOs;
using Puissance4.Application.Services;

namespace Puissance4.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        // Valide les informations d'identification
        if (loginDto is { Username: "admin", Password: "password" }) // Remplace par ton service AuthService
        {
            var token = _authService.GenerateToken(loginDto.Username);
            return Ok(new { Token = token });
        }
        return Unauthorized();
    }
}
*/