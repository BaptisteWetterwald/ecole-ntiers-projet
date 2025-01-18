namespace Blaze4Backend.DTOs;

public class GridDto
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public List<CellDto> Cells { get; set; } = new List<CellDto>();
}
