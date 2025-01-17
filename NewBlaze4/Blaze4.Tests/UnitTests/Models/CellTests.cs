using Blaze4Shared.Models;

namespace Blaze4.Tests.UnitTests.Models
{
    public class CellTests
    {
        [Fact]
        public void Cell_ShouldBeEmpty_OnInitialization()
        {
            // Arrange
            var cell = new Cell(0, 0);

            // Act & Assert
            Assert.Null(cell.Token);
        }

        [Fact]
        public void PlaceToken_ShouldSetToken_WhenCellIsEmpty()
        {
            // Arrange
            var cell = new Cell(0, 0);
            var token = new Token("Red");

            // Act
            cell.PlaceToken(token);

            // Assert
            Assert.Equal(token, cell.Token);
        }

        [Fact]
        public void PlaceToken_ShouldThrowException_WhenCellIsOccupied()
        {
            // Arrange
            var cell = new Cell(0, 0);
            var token1 = new Token("Red");
            var token2 = new Token("Yellow");
            cell.PlaceToken(token1);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => cell.PlaceToken(token2));
        }
    }
}