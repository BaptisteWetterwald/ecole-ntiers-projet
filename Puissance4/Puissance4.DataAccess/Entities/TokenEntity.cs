namespace Puissance4.DataAccess.Entities;

public class TokenEntity()
{
    public int Id { get; init; }
    public int CellId { get; set; }
    public required string Color { get; init; }
}