namespace Blaze4Backend.DTOs;

public class GameDto
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public PlayerDto? Host { get; set; }
    public PlayerDto? Guest { get; set; }
    public GridDto? Grid { get; set; }
}