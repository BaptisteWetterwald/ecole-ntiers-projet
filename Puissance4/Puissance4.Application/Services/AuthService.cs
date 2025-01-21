using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPlayerRepository _playerRepository;
    
    public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IPlayerRepository playerRepository)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _playerRepository = playerRepository;
    }
    
    public string GenerateToken(string username)
    {
        var user = _playerRepository.GetByLoginAsync(username).Result;
        if (user == null)
        {
            throw new Exception("Could not find user ID");
        }
        var userId = user.Id;
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim("UserId", userId.ToString()), // Ajoute l'ID de l'utilisateur dans le token
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public int GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value;
        if (userId == null)
        {
            throw new Exception("Could not find user ID");
        }
        return int.Parse(userId);
    }

    public string GetUsername()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
    }
}
