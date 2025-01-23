namespace Puissance4.DTOs;

public class GameDto
{
    public int Id { get; set; }
    public PlayerDto Host { get; set; }
    public PlayerDto? Guest { get; set; }
    public PlayerDto? Winner { get; set; }
    public PlayerDto? CurrentTurn { get; set; }
    public string Status { get; set; }
    public GridDto Grid { get; set; }
}