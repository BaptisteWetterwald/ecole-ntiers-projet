using Puissance4.Application.Domain;

namespace Puissance4.Tests.UnitTests.Domain;

public class CellTests
{
    [Fact]
    public void IsEmpty_ShouldReturnTrue_WhenCellHasNoToken()
    {
        var cell = new Cell(0, 0);
        Assert.True(cell.IsEmpty());
    }

    [Fact]
    public void PlaceToken_ShouldPlaceToken_WhenCellIsEmpty()
    {
        var cell = new Cell(0, 0);
        var token = new Token("Red");
        cell.PlaceToken(token);

        Assert.Equal(token, cell.Token);
    }

    [Fact]
    public void PlaceToken_ShouldThrowException_WhenCellIsOccupied()
    {
        var cell = new Cell(0, 0);
        var token = new Token("Red");
        cell.PlaceToken(token);

        Assert.Throws<InvalidOperationException>(() => cell.PlaceToken(new Token("Yellow")));
    }
}