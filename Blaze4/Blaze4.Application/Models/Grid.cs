namespace Blaze4.Application.Models;

public class Grid
{
    public const int Rows = 6;
    public const int Columns = 7;
    public Cell[,] Cells { get; set; } = new Cell[Rows, Columns];

    public Player Host { get; set; }
    public Player Guest { get; set; }

    public Grid()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                Cells[row, col] = new Cell(row, col);
            }
        }
    }

    public bool DropToken(int column, Token token)
    {
        for (int row = Rows - 1; row >= 0; row--)
        {
            if (Cells[row, column].Token == null)
            {
                Cells[row, column].Token = token;
                return CheckWin();
            }
        }
        throw new InvalidOperationException("This column is full.");
    }

    public bool IsFull()
    {
        foreach (var cell in Cells)
        {
            if (cell.Token == null) return false;
        }
        return true;
    }

    public bool CheckWin()
    {
        // Vérification des lignes (horizontal)
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns - 3; col++) // Vérifier 4 cases à partir de chaque colonne
            {
                if (Cells[row, col].Token != null &&
                    Cells[row, col].Token == Cells[row, col + 1].Token &&
                    Cells[row, col].Token == Cells[row, col + 2].Token &&
                    Cells[row, col].Token == Cells[row, col + 3].Token)
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
                if (Cells[row, col].Token != null &&
                    Cells[row, col].Token == Cells[row + 1, col].Token &&
                    Cells[row, col].Token == Cells[row + 2, col].Token &&
                    Cells[row, col].Token == Cells[row + 3, col].Token)
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
                if (Cells[row, col].Token != null &&
                    Cells[row, col].Token == Cells[row + 1, col + 1].Token &&
                    Cells[row, col].Token == Cells[row + 2, col + 2].Token &&
                    Cells[row, col].Token == Cells[row + 3, col + 3].Token)
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
                if (Cells[row, col].Token != null &&
                    Cells[row, col].Token == Cells[row - 1, col + 1].Token &&
                    Cells[row, col].Token == Cells[row - 2, col + 2].Token &&
                    Cells[row, col].Token == Cells[row - 3, col + 3].Token)
                {
                    return true; // Victoire diagonale bas-gauche à haut-droit
                }
            }
        }

        // Si aucune condition de victoire n'est remplie
        return false;
    }
}
