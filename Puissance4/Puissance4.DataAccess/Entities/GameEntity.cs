namespace Puissance4.DataAccess.Entities;

public class GameEntity
{
    public int Id { get; set; }
    
    public int GridId { get; set; } 
    public GridEntity Grid { get; set; }
    
    public required int HostId { get; set; }
    public PlayerEntity Host { get; set; }
    
    public int? GuestId { get; set; }
    public PlayerEntity? Guest { get; set; }
    
    public required string Status { get; set; }
    
    public int? WinnerId { get; set; }
    public PlayerEntity? Winner { get; set; }
    
    public int? CurrentTurnId { get; set; }
    public PlayerEntity? CurrentTurn { get; set; }
}