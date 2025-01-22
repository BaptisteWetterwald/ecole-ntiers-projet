using System.Text;

namespace Puissance4.DataAccess.Entities;

public class EFGrid
{
    public int Id { get; set; } // Clé primaire
    public int Rows { get; set; } // Nombre de lignes
    public int Columns { get; set; } // Nombre de colonnes

    // Relation avec les cellules
    public ICollection<EFCell> Cells { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine();
        var cells = new string[Rows, Columns];
        for (int i=0; i<Rows; i++)
        {
            for (int j=0; j<Columns; j++)
            {
                cells[i, j] = ".";
            }
        }
        foreach (var cell in Cells)
        {
            cells[cell.Row, cell.Column] = cell.TokenColor;
        }
        for (int i=0; i<Rows; i++)
        {
            for (int j=0; j<Columns; j++)
            {
                sb.Append(cells[i, j]);
                sb.Append('|');
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}