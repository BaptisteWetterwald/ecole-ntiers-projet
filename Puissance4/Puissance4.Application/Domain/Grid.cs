﻿using System.Text;

namespace Puissance4.Application.Domain;

public class Grid
{
    public Cell[,] Cells;
    public int Columns = 7;
    public int Rows = 6;

    public Grid()
    {
        Cells = new Cell[Rows, Columns];
        for (var row = 0; row < Rows; row++)
        for (var column = 0; column < Columns; column++)
            Cells[row, column] = new Cell(row, column);
    }

    public bool DropToken(int column, Token token)
    {
        for (var row = Rows - 1; row >= 0; row--)
        {
            if (!Cells[row, column].IsEmpty()) continue;
            Cells[row, column].PlaceToken(token);
            return true;
        }

        throw new InvalidOperationException("Column is full.");
    }

    public bool CheckWin()
    {
        // Vérification des lignes (horizontal)
        for (var row = 0; row < Rows; row++)
        for (var col = 0; col < Columns - 3; col++) // Vérifier 4 cases à partir de chaque colonne
        {
            // Vérifier si les 4 cases sont du même joueur, use .Equals() to compare Token objects
            var token = Cells[row, col].Token;
            if (token != null &&
                token.Equals(Cells[row, col + 1].Token) &&
                token.Equals(Cells[row, col + 2].Token) &&
                token.Equals(Cells[row, col + 3].Token))
                return true; // Victoire horizontale
        }

        // Vérification des colonnes (vertical)
        for (var col = 0; col < Columns; col++)
        for (var row = 0; row < Rows - 3; row++) // Vérifier 4 cases à partir de chaque ligne
        {
            var token = Cells[row, col].Token;
            if (token != null &&
                token.Equals(Cells[row + 1, col].Token) &&
                token.Equals(Cells[row + 2, col].Token) &&
                token.Equals(Cells[row + 3, col].Token))
                return true; // Victoire verticale
        }

        // Vérification des diagonales (haut-gauche à bas-droit)
        for (var row = 0; row < Rows - 3; row++)
        for (var col = 0; col < Columns - 3; col++) // Vérifier 4 cases diagonales
        {
            var token = Cells[row, col].Token;
            if (token != null &&
                token.Equals(Cells[row + 1, col + 1].Token) &&
                token.Equals(Cells[row + 2, col + 2].Token) &&
                token.Equals(Cells[row + 3, col + 3].Token))
                return true; // Victoire diagonale haut-gauche à bas-droit
        }

        // Vérification des diagonales (bas-gauche à haut-droit)
        for (var row = 3; row < Rows; row++) // On commence à partir de la 4ème ligne pour cette direction
        for (var col = 0; col < Columns - 3; col++) // Vérifier 4 cases diagonales
        {
            var token = Cells[row, col].Token;
            if (token != null &&
                token.Equals(Cells[row - 1, col + 1].Token) &&
                token.Equals(Cells[row - 2, col + 2].Token) &&
                token.Equals(Cells[row - 3, col + 3].Token))
                return true; // Victoire diagonale bas-gauche à haut-droit
        }

        // Si aucune condition de victoire n'est remplie
        return false;
    }

    public bool IsFull()
    {
        return Cells.Cast<Cell>().All(cell => !cell.IsEmpty());
    }

    public override string ToString()
    {
        // Displays the first letter of the color of each token in the grid, or a . if the cell is empty, separate each column by a | and each row by a newline

        var sb = new StringBuilder();
        sb.AppendLine();
        for (var row = 0; row < Rows; row++)
        {
            for (var col = 0; col < Columns; col++)
            {
                sb.Append(Cells[row, col].Token?.Color[0] ?? '.');
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

        return Cells[row, column];
    }
}