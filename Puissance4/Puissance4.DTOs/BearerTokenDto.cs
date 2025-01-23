namespace Puissance4.DTOs;

public class BearerTokenDto
{
    public string Type { get; set; } = "Bearer";
    public string Token { get; set; }
}