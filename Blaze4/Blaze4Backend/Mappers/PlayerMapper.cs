using Blaze4.Application.Models;
using Blaze4Backend.DTOs;

namespace Blaze4Backend.Mappers;

// Map the DB entities to the application models
public static class PlayerMapper
{
    public static PlayerDto ToDto(Player player)
    {
        return new PlayerDto
        {
            Login = player.Login
        };
    }
}