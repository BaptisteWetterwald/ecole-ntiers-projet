namespace Puissance4.DataAccess.Entities;

public class GameEntity
{
    public int Id { get; set; }
    public required int HostId { get; set; }
    public int? GuestId { get; set; }
    public int GridId { get; set; }
    public required string Status { get; set; }
    public int? WinnerId { get; set; }
    public int? CurrentTurnId { get; set; }
}