using Blaze4.Application.Models;
using FluentAssertions;
using Xunit;

namespace Blaze4.Tests
{
    public class GameTests
    {
        [Fact]
        public void Game_StartGame_ShouldSetStatusToInProgress_WhenGuestIsPresent()
        {
            // Arrange
            var host = new Player { Login = "HostPlayer" };
            var guest = new Player { Login = "GuestPlayer" };
            var game = new Game { Host = host };

            // Act
            game.JoinGame(guest);
            game.StartGame();

            // Assert
            game.Status.Should().Be("In Progress");
        }

        [Fact]
        public void Game_JoinGame_ShouldAssignGuest_WhenNoGuestIsPresent()
        {
            // Arrange
            var host = new Player { Login = "HostPlayer" };
            var guest = new Player { Login = "GuestPlayer" };
            var game = new Game { Host = host };

            // Act
            game.JoinGame(guest);

            // Assert
            game.Guest.Should().Be(guest);
            game.Status.Should().Be("Awaiting Guest");
        }

        [Fact]
        public void Grid_DropToken_ShouldAddTokenToCorrectColumn()
        {
            // Arrange
            var grid = new Grid();
            var token = new Token { Color = "Red" };

            // Act
            grid.DropToken(0, token);

            // Assert
            grid.Cells[5, 0].Token.Should().Be(token);
        }

        [Fact]
        public void Grid_IsFull_ShouldReturnTrue_WhenGridIsFull()
        {
            // Arrange
            var grid = new Grid();
            for (int col = 0; col < Grid.Columns; col++)
            {
                for (int row = 0; row < Grid.Rows; row++)
                {
                    grid.Cells[row, col].Token = new Token { Color = "Red" };
                }
            }

            // Act
            var result = grid.IsFull();

            // Assert
            result.Should().BeTrue();
        }
    }
}