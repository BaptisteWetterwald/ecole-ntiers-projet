using Blaze4.Application.Models;
using Blaze4Backend.DTOs;

namespace Blaze4Backend.Mappers;

// Map the DB entities to the application models
public static class GameMapper
{
    public static GameDto ToDto(Game game)
    {
        return new GameDto
        {
            Id = game.Id,
            Status = game.Status,
            Host = PlayerMapper.ToDto(game.Host),
            Guest = game.Guest != null ? PlayerMapper.ToDto(game.Guest) : null,
            Grid = GridMapper.ToDto(game.Grid)
        };
    }
}