namespace Blaze4Shared.Models;

public class Cell
{
    public int Row { get; }
    public int Column { get; }
    public Token? Token { get; private set; }

    public Cell(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public bool IsEmpty()
    {
        return Token == null;
    }

    public void PlaceToken(Token token)
    {
        if (!IsEmpty())
            throw new InvalidOperationException("Cell is already occupied.");

        Token = token;
    }
}
