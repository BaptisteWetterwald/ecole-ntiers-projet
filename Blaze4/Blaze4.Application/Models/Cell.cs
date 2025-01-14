namespace Blaze4.Application.Models;

public class Cell
{
    
    /*
     * class Cell {
        +int row
        +int column
        +Token token
    }
     */
    
    public int Row { get; }
    public int Column { get; }
    public Token? Token { get; set; }

    public Cell(int row, int column)
    {
        Row = row;
        Column = column;
    }
}