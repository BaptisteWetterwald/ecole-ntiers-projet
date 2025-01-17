using System.Text;

namespace Blaze4Shared.Models;

public class Grid
{
    public const int Rows = 6;
    public const int Columns = 7;
    private readonly Cell[,] _cells;

    public Grid()
    {
        _cells = new Cell[Rows, Columns];
        for (int row = 0; row < Rows; row++)
        for (int col = 0; col < Columns; col++)
            _cells[row, col] = new Cell(row, col);
    }

    public bool DropToken(int column, Token token)
    {
        for (int row = Rows - 1; row >= 0; row--)
        {
            if (_cells[row, column].IsEmpty())
            {
                _cells[row, column].PlaceToken(token);
                return true;
            }
        }
        throw new InvalidOperationException("Column is full.");
    }

    public bool CheckWin()
    {
        // Vérification des lignes (horizontal)
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns - 3; col++) // Vérifier 4 cases à partir de chaque colonne
            {
                // Vérifier si les 4 cases sont du même joueur, use .Equals() to compare Token objects
                var token = _cells[row, col].Token;
                if (token != null &&
                    token.Equals(_cells[row, col + 1].Token) &&
                    token.Equals(_cells[row, col + 2].Token) &&
                    token.Equals(_cells[row, col + 3].Token))
                {
                    return true; // Victoire horizontale
                }
            }
        }
        
        // Vérification des colonnes (vertical)
        for (int col = 0; col < Columns; col++)
        {
            for (int row = 0; row < Rows - 3; row++) // Vérifier 4 cases à partir de chaque ligne
            {
                var token = _cells[row, col].Token;
                if (token != null &&
                    token.Equals(_cells[row + 1, col].Token) &&
                    token.Equals(_cells[row + 2, col].Token) &&
                    token.Equals(_cells[row + 3, col].Token))
                {
                    return true; // Victoire verticale
                }
            }
        }

        // Vérification des diagonales (haut-gauche à bas-droit)
        for (int row = 0; row < Rows - 3; row++)
        {
            for (int col = 0; col < Columns - 3; col++) // Vérifier 4 cases diagonales
            {
                var token = _cells[row, col].Token;
                if (token != null &&
                    token.Equals(_cells[row + 1, col + 1].Token) &&
                    token.Equals(_cells[row + 2, col + 2].Token) &&
                    token.Equals(_cells[row + 3, col + 3].Token))
                {
                    return true; // Victoire diagonale haut-gauche à bas-droit
                }
            }
        }

        // Vérification des diagonales (bas-gauche à haut-droit)
        for (int row = 3; row < Rows; row++) // On commence à partir de la 4ème ligne pour cette direction
        {
            for (int col = 0; col < Columns - 3; col++) // Vérifier 4 cases diagonales
            {
                var token = _cells[row, col].Token;
                if (token != null &&
                    token.Equals(_cells[row - 1, col + 1].Token) &&
                    token.Equals(_cells[row - 2, col + 2].Token) &&
                    token.Equals(_cells[row - 3, col + 3].Token))
                {
                    return true; // Victoire diagonale bas-gauche à haut-droit
                }
            }
        }

        // Si aucune condition de victoire n'est remplie
        return false;
    }

    public bool IsFull()
    {
        return _cells.Cast<Cell>().All(cell => !cell.IsEmpty());
    }
    
    public override string ToString()
    {
        // Displays the first letter of the color of each token in the grid, or a . if the cell is empty, separate each column by a | and each row by a newline
        
        var sb = new StringBuilder();
        sb.AppendLine();
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                sb.Append(_cells[row, col].Token?.Color[0] ?? '.');
                sb.Append('|');
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
    
    public Cell GetCell(int row, int column)
    {
        if (row < 0 || row >= Rows || column < 0 || column >= Columns)
            throw new ArgumentOutOfRangeException("Invalid cell coordinates.");

        return _cells[row, column];
    }
}
