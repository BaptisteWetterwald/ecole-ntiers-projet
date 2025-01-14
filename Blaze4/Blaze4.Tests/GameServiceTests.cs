using Blaze4.Application.Models;
using Blaze4.Application.Services;
using FluentAssertions;
using Xunit;

namespace Blaze4.Tests
{
    public class GameServiceTests
    {
        [Fact]
        public void CreateGame_ShouldAddNewGame()
        {
            // Arrange
            var service = new GameService();
            var host = new Player { Login = "Alice" };

            // Act
            var game = service.CreateGame(host);

            // Assert
            game.Should().NotBeNull();
            game.Host.Should().Be(host);
            game.Status.Should().Be(Game.AwaitingGuest);
        }

        [Fact]
        public void JoinGame_ShouldAssignGuestAndChangeStatus()
        {
            // Arrange
            var service = new GameService();
            var host = new Player { Login = "Alice" };
            var guest = new Player { Login = "Bob" };
            var game = service.CreateGame(host);

            // Act
            var joinedGame = service.JoinGame(guest, game.Id);

            // Assert
            joinedGame.Should().NotBeNull();
            joinedGame!.Guest.Should().Be(guest);
            joinedGame.Status.Should().Be(Game.InProgress);
            joinedGame.Id.Should().Be(game.Id);
        }

        [Fact]
        public void GetGamesAwaitingGuest_ShouldReturnGamesWithNoGuest()
        {
            // Arrange
            var service = new GameService();
            var player = new Player { Login = "Alice" };
            service.CreateGame(player);
            service.CreateGame(player);

            // Act
            var games = service.GetGamesAwaitingGuest();

            // Assert
            games.Should().HaveCount(2);
            games.All(g => g.Status == Game.AwaitingGuest).Should().BeTrue();
        }

        [Fact]
        public void GetGamesAwaitingPlayerAction_ShouldReturnGamesWaitingForPlayer()
        {
            // Arrange
            var service = new GameService();
            var player = new Player { Login = "Alice" };
            var game = service.CreateGame(player);
            game.Status = Game.InProgress;
            game.Guest = new Player { Login = "Bob" };

            // Act
            var games = service.GetGamesAwaitingPlayerAction(player);

            // Assert
            games.Should().HaveCount(1);
            games.First().Status.Should().Be(Game.InProgress);
        }

        [Fact]
        public void PlayTurn_ShouldPlayTurnAndSwitch()
        {
            // Arrange
            var service = new GameService();
            var host = new Player { Login = "Alice" };
            var guest = new Player { Login = "Bob" };
            var game = service.CreateGame(host);
            service.JoinGame(guest, game.Id);
            
            service.StartGame(game.Id);

            // Act
            var result = service.PlayTurn(game.Id, host, 0);

            // Assert
            result.Should().Be("Turn played successfully.");
            game.Grid.Cells[5, 0].Token.Should().NotBeNull();
            game.Grid.Cells[5, 0].Token.Color.Should().Be("Red");
        }

        [Fact]
        public void PlayTurn_ShouldNotAllowWrongPlayerToPlay()
        {
            // Arrange
            var service = new GameService();
            var host = new Player { Login = "Alice" };
            var guest = new Player { Login = "Bob" };
            var game = service.CreateGame(host);
            service.JoinGame(guest, game.Id);

            // Act
            var result = service.PlayTurn(game.Id, host, 0);

            // Assert
            result.Should().Be("It's not your turn.");
        }

        [Fact]
        public void GetGamesAwaitingPlayerAction_ShouldReturnGamesInProgress()
        {
            // Arrange
            var service = new GameService();
            var host = new Player { Login = "Alice" };
            var guest = new Player { Login = "Bob" };
            var game = service.CreateGame(host);
            service.JoinGame(guest, game.Id);
            service.PlayTurn(game.Id, host, 0);

            // Act
            var games = service.GetGamesAwaitingPlayerAction(guest);

            // Assert
            games.Should().HaveCount(1);
            games.First().Status.Should().Be(Game.InProgress);
        }

        [Fact]
        public void PlayTurn_ShouldEndGameWhenWinConditionMet()
        {
            // Arrange
            var service = new GameService();
            var host = new Player { Login = "Alice" };
            var guest = new Player { Login = "Bob" };
            var game = service.CreateGame(host);
            service.JoinGame(guest, game.Id);
            
            service.StartGame(game.Id);

            // Simuler un scénario où le joueur gagne en alignant 4 pions
            // On va remplir la grille pour qu'un joueur gagne (par exemple, colonne 0)
            service.PlayTurn(game.Id, host, 0); // Alice
            service.PlayTurn(game.Id, guest, 1); // Bob
            service.PlayTurn(game.Id, host, 0); // Alice
            service.PlayTurn(game.Id, guest, 1); // Bob
            service.PlayTurn(game.Id, host, 0); // Alice
            service.PlayTurn(game.Id, guest, 1); // Bob
            var result = service.PlayTurn(game.Id, host, 0); // Alice gagne ici

            // Assert
            result.Should().Contain("wins");
            game.Status.Should().Be(Game.Finished);
        }

        /*
         [Fact]
        public void PlayTurn_ShouldEndGameWhenDraw()
        {
            // Arrange
            var service = new GameService();
            var host = new Player { Login = "Alice" };
            var guest = new Player { Login = "Bob" };
            var game = service.CreateGame(host);
            service.JoinGame(guest, game.Id);

            // Simuler un scénario où la grille est pleine sans vainqueur
            for (int i = 0; i < Grid.Rows * Grid.Columns; i++)
            {
                var column = i % Grid.Columns;
                service.PlayTurn(game.Id, i % 2 == 0 ? host : guest, column);
            }

            // Act
            var result = service.PlayTurn(game.Id, host, 0); // Dernier tour

            // Assert
            result.Should().Be("It's a draw!");
            game.Status.Should().Be("Finished");
        }
        */
    }
}
