namespace Puissance4.DataAccess.Entities;

public class GridEntity
{
    public int Id { get; set; }
    public int Rows = 6;
    public int Columns = 7;
    public List<CellEntity> Cells { get; set; } = new();
    //public int[,] CellsId { get; set; }
}