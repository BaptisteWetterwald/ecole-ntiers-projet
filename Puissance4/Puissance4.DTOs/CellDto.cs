namespace Puissance4.DTOs;

public class CellDto
{
    public int Row { get; set; }
    public int Column { get; set; }
    public string? TokenColor { get; set; } // Simplifie l'accès au Token
}
