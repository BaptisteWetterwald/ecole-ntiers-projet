using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using Puissance4.Application.Services;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;
using Puissance4.DTOs;

namespace Puissance4.Tests.UnitTests.Services;

public class AuthServiceTests
{
    private readonly AuthService _authService;
    private readonly Mock<IConfiguration> _configurationMock = new();
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();

    public AuthServiceTests()
    {
        _authService = new AuthService(
            _configurationMock.Object,
            _httpContextAccessorMock.Object,
            _playerRepositoryMock.Object
        );
    }

    [Fact]
    public async Task GenerateToken_ShouldReturnToken_WhenUserExists()
    {
        var user = new EFPlayer { Id = 1, Login = "User" };
        _playerRepositoryMock.Setup(r => r.GetByLoginAsync("User")).ReturnsAsync(user);

        // Utiliser une clé brute d'au moins 32 caractères
        _configurationMock.Setup(c => c["Jwt:Key"]).Returns("SuperSecretKeyThatIsLongEnoughToBe256Bits");
        _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("Issuer");
        _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("Audience");

        var token = await _authService.GenerateToken("User");

        Assert.NotNull(token);
        Assert.IsType<string>(token);
    }


    [Fact]
    public void GetUserId_ShouldThrowException_WhenUserIdNotFound()
    {
        _httpContextAccessorMock.Setup(a => a.HttpContext.User.FindFirst("UserId"))
            .Returns((Claim)null);

        Assert.Throws<Exception>(() => _authService.GetUserId());
    }

    [Fact]
    public async Task VerifyPassword_ShouldReturnTrue_WhenPasswordIsCorrect()
    {
        var password = "CorrectPassword";
        var hashedPassword = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));

        var user = new EFPlayer
        {
            Login = "User",
            PasswordHash = hashedPassword
        };

        _playerRepositoryMock.Setup(r => r.GetByLoginAsync("User")).ReturnsAsync(user);

        var loginDto = new LoginDto { Username = "User", Password = "CorrectPassword" };

        var result = await _authService.VerifyPassword(loginDto);
        Assert.True(result);
    }
}