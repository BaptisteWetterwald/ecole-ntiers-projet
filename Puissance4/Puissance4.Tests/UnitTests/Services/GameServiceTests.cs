using Moq;
using Puissance4.Application.Services;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Tests.UnitTests.Services;

public class GameServiceTests
{
    private readonly Mock<ICellRepository> _cellRepositoryMock = new();
    private readonly Mock<IGameRepository> _gameRepositoryMock = new();
    private readonly GameService _gameService;
    private readonly Mock<IGridRepository> _gridRepositoryMock = new();
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();

    public GameServiceTests()
    {
        _gameService = new GameService(
            _gameRepositoryMock.Object,
            _playerRepositoryMock.Object,
            _gridRepositoryMock.Object,
            _cellRepositoryMock.Object
        );
    }

    [Fact]
    public async Task CreateGame_ShouldReturnGameDto_WhenHostExists()
    {
        var host = new EFPlayer { Id = 1, Login = "Host" };
        _playerRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(host);

        var result = await _gameService.CreateGame(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Host.Id);
    }

    [Fact]
    public async Task JoinGame_ShouldThrowException_WhenGameNotFound()
    {
        _gameRepositoryMock.Setup(r => r.GetGameWithGridAsync(1)).ReturnsAsync((EFGame)null);

        await Assert.ThrowsAsync<ArgumentException>(() => _gameService.JoinGame(1, 2));
    }

    [Fact]
    public async Task PlayTurn_ShouldThrowException_WhenGameNotFound()
    {
        _gameRepositoryMock.Setup(r => r.GetGameWithGridAsync(1)).ReturnsAsync((EFGame)null);

        await Assert.ThrowsAsync<ArgumentException>(() => _gameService.PlayTurn(1, 1, 0));
    }
}