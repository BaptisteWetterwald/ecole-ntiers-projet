using Blaze4.Domain.Models;

namespace Blaze4.Tests.UnitTests
{
    public class TokenTests
    {
        [Fact]
        public void Tokens_WithSameColor_ShouldBeEqual()
        {
            // Arrange
            var token1 = new Token("Red");
            var token2 = new Token("Red");

            // Act & Assert
            Assert.Equal(token1, token2);
        }

        [Fact]
        public void Tokens_WithDifferentColors_ShouldNotBeEqual()
        {
            // Arrange
            var token1 = new Token("Red");
            var token2 = new Token("Yellow");

            // Act & Assert
            Assert.NotEqual(token1, token2);
        }

        [Fact]
        public void Token_GetHashCode_ShouldBeBasedOnColor()
        {
            // Arrange
            var token = new Token("Red");

            // Act
            var hashCode = token.GetHashCode();

            // Assert
            Assert.Equal("Red".GetHashCode(), hashCode);
        }
    }
}