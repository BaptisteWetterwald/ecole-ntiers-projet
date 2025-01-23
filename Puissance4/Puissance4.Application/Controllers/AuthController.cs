using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puissance4.Application.Services;
using Puissance4.DTOs;

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
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        Console.WriteLine("AuthController: Login(" + loginDto.Username + ")");
        Console.WriteLine("Password: " + loginDto.Password);

        var correctPwd = await _authService.VerifyPassword(loginDto);
        if (!correctPwd) return Unauthorized();

        var token = await _authService.GenerateToken(loginDto.Username);
        return Ok(new BearerTokenDto { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginDto loginDto)
    {
        await _authService.Register(loginDto);
        var token = await _authService.GenerateToken(loginDto.Username);
        return Ok(new BearerTokenDto { Token = token });
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        _authService.Logout();
        return Ok();
    }
}