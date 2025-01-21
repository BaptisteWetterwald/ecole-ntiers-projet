namespace Puissance4.Application.DTOs;

public class BearerTokenDto
{
    public string Type { get; set; } = "Bearer";
    public string Token { get; set; }
}