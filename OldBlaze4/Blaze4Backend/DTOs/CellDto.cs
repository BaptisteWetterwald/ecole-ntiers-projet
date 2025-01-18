namespace Blaze4Backend.DTOs;

public class CellDto
{
    public int Row { get; set; }
    public int Column { get; set; }
    public TokenDto? Token { get; set; }
}
