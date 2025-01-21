namespace Puissance4.Application.DTOs;

public class GridDto
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public CellDto[,] Cells { get; set; } // Tableau 2D simplifié
}
