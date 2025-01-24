using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;
using Puissance4.DTOs;

namespace Puissance4.Application.Services;

public class AuthService
{
    /* Postman
    {
        "username": "Baptouste",
        "password": "#qlflop"
    }
    {
        "username": "Kepplouf",
        "password": "Thomsoja"
    }
    */

    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPlayerRepository _playerRepository;

    public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor,
        IPlayerRepository playerRepository)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _playerRepository = playerRepository;
    }

    /*
    public async Task<string> GenerateToken(string username)
    {
        var user = await _playerRepository.GetByLoginAsync(username);
        if (user == null) throw new Exception("Could not find user ID");
        var userId = user.Id;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim("UserId", userId.ToString()), // Ajoute l'ID de l'utilisateur dans le token
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var rawKey = _configuration["Jwt:Key"];
        var keyBytes = Convert.FromBase64String(rawKey.Trim()); // Décodage Base64
        var key = new SymmetricSecurityKey(keyBytes);

        //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));  //Blaze4LeProjetPuissance4DeLaMortQuiTue
        //var key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)); // 32 octets = 256 bits
        Console.WriteLine($"Generated Key: {key}");

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    */

    public async Task<string> GenerateToken(string username)
    {
        var user = await _playerRepository.GetByLoginAsync(username);
        if (user == null) throw new Exception("Could not find user ID");
        var userId = user.Id;

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim("UserId", userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var rawKey = _configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(rawKey) || rawKey.Length < 32)
            throw new ArgumentException("The JWT key must be at least 32 characters long.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(rawKey));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public int GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value;
        if (userId == null) throw new Exception("Could not find user ID");
        return int.Parse(userId);
    }


    private string HashPassword(string password)
    {
        return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
    }

    public async Task<bool> VerifyPassword(LoginDto loginDto)
    {
        var user = await _playerRepository.GetByLoginAsync(loginDto.Username);
        if (user == null) return false;
        return user.PasswordHash == HashPassword(loginDto.Password);
    }

    public void Logout()
    {
        _httpContextAccessor.HttpContext?.SignOutAsync();
    }

    public async Task Register(LoginDto loginDto)
    {
        var user = _playerRepository.GetByLoginAsync(loginDto.Username).Result;
        if (user != null) throw new Exception("User already exists");

        var newUser = new EFPlayer
        {
            Login = loginDto.Username,
            PasswordHash = HashPassword(loginDto.Password)
        };

        await _playerRepository.AddAsync(newUser);
    }
}