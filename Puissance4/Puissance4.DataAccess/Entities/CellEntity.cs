namespace Puissance4.DataAccess.Entities;

public class CellEntity()
{
    public int Id { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public int GridId { get; set; }
    
    public int? TokenId { get; set; }
    public TokenEntity? Token { get; set; }
}