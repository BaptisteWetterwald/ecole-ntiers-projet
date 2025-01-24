using Puissance4.Application.Domain;

namespace Puissance4.Tests.UnitTests.Domain;

public class PlayerTests
{
    [Fact]
    public void Equals_ShouldReturnTrue_WhenPlayersHaveSameId()
    {
        var player1 = new Player { Id = 1, Login = "Player1" };
        var player2 = new Player { Id = 1, Login = "Player2" };

        Assert.True(player1.Equals(player2));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenPlayersHaveDifferentIds()
    {
        var player1 = new Player { Id = 1, Login = "Player1" };
        var player2 = new Player { Id = 2, Login = "Player2" };

        Assert.False(player1.Equals(player2));
    }
}