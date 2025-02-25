﻿namespace Puissance4.Application.Domain;

public class Cell
{
    public Cell(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Row { get; set; }
    public int Column { get; set; }
    public Token? Token { get; set; }

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