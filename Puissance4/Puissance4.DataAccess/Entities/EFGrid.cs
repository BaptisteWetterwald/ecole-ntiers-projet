namespace Puissance4.DataAccess.Entities;

public class EFGrid
{
    public int Id { get; set; } // Clé primaire
    public int Rows { get; set; } // Nombre de lignes
    public int Columns { get; set; } // Nombre de colonnes

    // Relation avec les cellules
    public ICollection<EFCell> Cells { get; set; }
}