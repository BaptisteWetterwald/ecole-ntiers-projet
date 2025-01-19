using Puissance4.Application.Domain;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class GameMapper
{
    public static GameEntity ToEntity(Game game)
    {
        return new GameEntity
        {
            Id = game.Id,
            GridId = game.Grid.Id,
            Grid = GridMapper.ToEntity(game.Grid),
            Status = game.Status,
            HostId = game.Host.Id,
            Host = PlayerMapper.ToEntity(game.Host),
            GuestId = game.Guest?.Id,
            Guest = game.Guest == null ? null : PlayerMapper.ToEntity(game.Guest),
            WinnerId = game.Winner?.Id,
            Winner = game.Winner == null ? null : PlayerMapper.ToEntity(game.Winner),
            CurrentTurnId = game.CurrentTurn?.Id,
            CurrentTurn = game.CurrentTurn == null ? null : PlayerMapper.ToEntity(game.CurrentTurn)
        };
    }
    
    public static Game ToDomain(GameEntity entity, Player host, Player? guest, Grid grid, Player? winner, Player? currentTurn)
    { 
        return new Game
        {
            Id = entity.Id,
            Status = entity.Status,
            Host = host,
            Guest = guest,
            Grid = grid,
            Winner = winner,
            CurrentTurn = currentTurn
        };
    }
}