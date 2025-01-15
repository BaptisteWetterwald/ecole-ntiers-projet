namespace Blaze4.Application.Models;

public class Token
{
    
    /*
     * class Token {
        +String color
    }
     */
    public string Color { get; set; }
    
    // override object.Equals
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Token token = (Token) obj;
        return Color == token.Color;
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
        return Color.GetHashCode();
    }
    
}