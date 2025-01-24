using Puissance4.Application.Domain;

namespace Puissance4.Tests.UnitTests.Domain;

public class GameTests
{
    [Fact]
    public void JoinGame_ShouldSetGuestAndSwitchTurn_WhenGuestJoins()
    {
        var host = new Player { Id = 1, Login = "Host" };
        var guest = new Player { Id = 2, Login = "Guest" };
        var game = new Game(host);

        game.JoinGame(guest);

        Assert.Equal(guest, game.Guest);
        Assert.Equal(guest, game.CurrentTurn);
        Assert.Equal(Game.Statuses.InProgress, game.Status);
    }

    [Fact]
    public void PlayTurn_ShouldDropTokenAndSwitchTurn_WhenValidMove()
    {
        var host = new Player { Id = 1, Login = "Host" };
        var guest = new Player { Id = 2, Login = "Guest" };
        var game = new Game(host);
        game.JoinGame(guest);

        game.PlayTurn(guest, 0);

        Assert.NotNull(game.Grid.Cells[5, 0].Token);
        Assert.Equal(host, game.CurrentTurn);
    }

    [Fact]
    public void PlayTurn_ShouldThrowException_WhenNotPlayerTurn()
    {
        var host = new Player { Id = 1, Login = "Host" };
        var guest = new Player { Id = 2, Login = "Guest" };
        var game = new Game(host);
        game.JoinGame(guest);

        Assert.Throws<InvalidOperationException>(() => game.PlayTurn(host, 0));
    }
}