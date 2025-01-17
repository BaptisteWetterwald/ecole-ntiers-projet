using Blaze4Shared.Models;

namespace Blaze4.Tests.UnitTests.Models
{
    public class GridTests
    {
        [Fact]
        public void Grid_ShouldBeEmpty_OnInitialization()
        {
            // Arrange
            var grid = new Grid();

            // Act & Assert
            for (int row = 0; row < Grid.Rows; row++)
            {
                for (int col = 0; col < Grid.Columns; col++)
                {
                    Assert.True(grid.GetCell(row, col).IsEmpty());
                }
            }
        }

        [Fact]
        public void DropToken_ShouldPlaceToken_InCorrectColumn()
        {
            // Arrange
            var grid = new Grid();
            var token = new Token("Red");

            // Act
            grid.DropToken(3, token);

            // Assert
            Assert.Equal(token, grid.GetCell(Grid.Rows - 1, 3).Token);
        }

        [Fact]
        public void DropToken_ShouldStackTokens_InSameColumn()
        {
            // Arrange
            var grid = new Grid();
            var redToken = new Token("Red");
            var yellowToken = new Token("Yellow");

            // Act
            grid.DropToken(3, redToken);
            grid.DropToken(3, yellowToken);

            // Assert
            Assert.Equal(redToken, grid.GetCell(Grid.Rows - 1, 3).Token);
            Assert.Equal(yellowToken, grid.GetCell(Grid.Rows - 2, 3).Token);
        }

        [Fact]
        public void DropToken_ShouldThrowException_WhenColumnIsFull()
        {
            // Arrange
            var grid = new Grid();
            var token = new Token("Red");

            for (int i = 0; i < Grid.Rows; i++)
            {
                grid.DropToken(3, token);
            }

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => grid.DropToken(3, token));
        }

        [Fact]
        public void IsFull_ShouldReturnFalse_WhenGridIsNotCompletelyFilled()
        {
            // Arrange
            var grid = new Grid();
            var token = new Token("Red");

            // Act
            grid.DropToken(0, token);

            // Assert
            Assert.False(grid.IsFull());
        }

        [Fact]
        public void IsFull_ShouldReturnTrue_WhenAllCellsAreFilled()
        {
            // Arrange
            var grid = new Grid();
            var token = new Token("Red");

            for (int col = 0; col < Grid.Columns; col++)
            {
                for (int row = 0; row < Grid.Rows; row++)
                {
                    grid.DropToken(col, token);
                }
            }

            // Act & Assert
            Assert.True(grid.IsFull());
        }

        [Fact]
        public void CheckWin_ShouldDetectHorizontalVictory()
        {
            // Arrange
            var grid = new Grid();
            var token = new Token("Red");

            for (int col = 0; col < 4; col++)
            {
                grid.DropToken(col, token);
            }

            // Act & Assert
            Assert.True(grid.CheckWin());
        }

        [Fact]
        public void CheckWin_ShouldDetectVerticalVictory()
        {
            // Arrange
            var grid = new Grid();
            var token = new Token("Red");

            for (int row = 0; row < 4; row++)
            {
                grid.DropToken(0, token);
            }

            // Act & Assert
            Assert.True(grid.CheckWin());
        }

        [Fact]
        public void CheckWin_ShouldDetectDiagonalVictory()
        {
            // Arrange
            var grid = new Grid();
            var redToken = new Token("Red");
            var yellowToken = new Token("Yellow");

            // Fill grid to form a diagonal victory
            grid.DropToken(0, redToken);
            grid.DropToken(1, yellowToken);
            grid.DropToken(1, redToken);
            grid.DropToken(2, yellowToken);
            grid.DropToken(2, yellowToken);
            grid.DropToken(2, redToken);
            grid.DropToken(3, yellowToken);
            grid.DropToken(3, yellowToken);
            grid.DropToken(3, yellowToken);
            grid.DropToken(3, redToken);

            // Act & Assert
            Assert.True(grid.CheckWin());
        }
    }
}
