namespace Puissance4.Application.Domain;

public class Token
{
    public int Id { get; set; }
    public string Color { get; }

    public Token(string color)
    {
        if (color != "Red" && color != "Yellow")
        {
            throw new ArgumentException("Invalid token color. Allowed values are 'Red' or 'Yellow'.");
        }
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

    public override string ToString()
    {
        return Color;
    }
}
