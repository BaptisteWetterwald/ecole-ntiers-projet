using Puissance4.Application.Domain;
using Puissance4.Application.DTOs;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class GameMapper
{
    public static Game ToDomain(EFGame entity)
    {
        return new Game
        {
            Id = entity.Id,
            Grid = GridMapper.ToDomain(entity.Grid),
            Host = PlayerMapper.ToDomain(entity.Host),
            Guest = entity.Guest != null ? PlayerMapper.ToDomain(entity.Guest) : null,
            Winner = entity.Winner != null ? PlayerMapper.ToDomain(entity.Winner) : null,
            CurrentTurn = entity.CurrentTurn != null ? PlayerMapper.ToDomain(entity.CurrentTurn) : null,
            Status = entity.Status
        };
    }

    public static EFGame ToEntity(Game domain)
    {
        return new EFGame
        {
            Id = domain.Id,
            Grid = GridMapper.ToEntity(domain.Grid),
            HostId = domain.Host.Id,
            GuestId = domain.Guest?.Id,
            WinnerId = domain.Winner?.Id,
            CurrentTurnId = domain.CurrentTurn?.Id,
            Status = domain.Status
        };
    }
    
    public static GameDto ToDto(Game game)
    {
        return new GameDto
        {
            Id = game.Id,
            Host = PlayerMapper.ToDto(game.Host),
            Guest = game.Guest != null ? PlayerMapper.ToDto(game.Guest) : null,
            Winner = game.Winner != null ? PlayerMapper.ToDto(game.Winner) : null,
            Status = game.Status,
            Grid = GridMapper.ToDto(game.Grid)
        };
    }
}
