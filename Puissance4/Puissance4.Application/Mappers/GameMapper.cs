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
            Status = game.Status,
            HostId = game.Host.Id,
            GuestId = game.Guest?.Id,
            WinnerId = game.Winner?.Id,
            CurrentTurnId = game.CurrentTurn?.Id
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