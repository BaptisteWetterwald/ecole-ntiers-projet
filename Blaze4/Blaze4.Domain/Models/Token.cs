namespace Blaze4.Domain.Models;

public class Token
{
    public string Color { get; private set; }

    public Token(string color)
    {
        Color = color;
    }

    public override bool Equals(object? obj)
    {
        return obj is Token token && Color == token.Color;
    }

    public override int GetHashCode()
    {
        return Color.GetHashCode();
    }
}
