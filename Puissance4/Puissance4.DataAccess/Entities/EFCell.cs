namespace Puissance4.DataAccess.Entities;

public class EFCell
{
    public int Id { get; set; } // Clé primaire
    public int Row { get; set; } // Coordonnée de la ligne
    public int Column { get; set; } // Coordonnée de la colonne

    // Clé étrangère vers EFGrid
    public int GridId { get; set; }
    public EFGrid Grid { get; set; }

    public required string TokenColor { get; set; }
}