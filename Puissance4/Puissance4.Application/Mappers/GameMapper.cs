using Puissance4.Application.Domain;
using Puissance4.DTOs;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class GameMapper
{
    public static Game ToDomain(EFGame entity)
    {
        return new Game(PlayerMapper.ToDomain(entity.Host))
        {
            Id = entity.Id,
            Grid = GridMapper.ToDomain(entity.Grid),
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
            CurrentTurn = game.CurrentTurn != null ? PlayerMapper.ToDto(game.CurrentTurn) : null,
            Status = game.Status,
            Grid = GridMapper.ToDto(game.Grid)
        };
    }
    
    public static GameDto ToDto(EFGame entity)
    {
        if (entity.Host == null)
        {
            throw new ArgumentException("Host is required");
        }
        return new GameDto
        {
            Id = entity.Id,
            Host = PlayerMapper.ToDto(entity.Host),
            Guest = entity.Guest != null ? PlayerMapper.ToDto(entity.Guest) : null,
            Winner = entity.Winner != null ? PlayerMapper.ToDto(entity.Winner) : null,
            CurrentTurn = entity.CurrentTurn != null ? PlayerMapper.ToDto(entity.CurrentTurn) : null,
            Status = entity.Status,
            Grid = GridMapper.ToDto(entity.Grid)
        };
    }
}
