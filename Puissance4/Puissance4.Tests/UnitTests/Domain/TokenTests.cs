using Puissance4.Application.Domain;

namespace Puissance4.Tests.UnitTests.Domain;

public class TokenTests
{
    [Fact]
    public void Constructor_ShouldThrowException_WhenColorIsInvalid()
    {
        Assert.Throws<ArgumentException>(() => new Token("Blue"));
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenTokensHaveSameColor()
    {
        var token1 = new Token("Red");
        var token2 = new Token("Red");

        Assert.True(token1.Equals(token2));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenTokensHaveDifferentColors()
    {
        var token1 = new Token("Red");
        var token2 = new Token("Yellow");

        Assert.False(token1.Equals(token2));
    }
}