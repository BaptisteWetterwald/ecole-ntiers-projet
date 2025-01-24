using Puissance4.Application.Domain;

namespace Puissance4.Tests.UnitTests.Domain;

public class GridTests
{
    [Fact]
    public void DropToken_ShouldPlaceTokenInCorrectCell_WhenColumnIsNotFull()
    {
        var grid = new Grid();
        var token = new Token("Red");

        grid.DropToken(0, token);

        Assert.Equal(token, grid.Cells[5, 0].Token);
    }

    [Fact]
    public void DropToken_ShouldThrowException_WhenColumnIsFull()
    {
        var grid = new Grid();
        for (var i = 0; i < grid.Rows; i++) grid.DropToken(0, new Token("Red"));

        Assert.Throws<InvalidOperationException>(() => grid.DropToken(0, new Token("Red")));
    }

    [Fact]
    public void CheckWin_ShouldReturnTrue_WhenFourInARowHorizontally()
    {
        var grid = new Grid();
        var token = new Token("Red");

        for (var i = 0; i < 4; i++) grid.DropToken(i, token);

        Assert.True(grid.CheckWin());
    }
}