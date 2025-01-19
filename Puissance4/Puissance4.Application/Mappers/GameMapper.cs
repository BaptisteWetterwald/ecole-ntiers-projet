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
            GridId = game.Grid.Id, // Mapper uniquement l'ID de la grille
            Status = game.Status,
            HostId = game.Host.Id, // Mapper uniquement l'ID de l'hôte
            GuestId = game.Guest?.Id,
            WinnerId = game.Winner?.Id,
            CurrentTurnId = game.CurrentTurn?.Id
        };
    }
    
    public static Game ToDomain(GameEntity entity, Func<PlayerEntity, Player> mapPlayer)
    {
        return new Game
        {
            Id = entity.Id,
            Status = entity.Status,
            Host = mapPlayer(entity.Host), // Mapper l'hôte
            Guest = entity.Guest != null ? mapPlayer(entity.Guest) : null, // Mapper l'invité optionnellement
            Grid = GridMapper.ToDomain(entity.Grid) // Mapper la grille entièrement
        };
    }


    /*public static Game ToDomain(GameEntity entity, Func<int, Player> getPlayerById, Func<int, Grid> getGridById)
    {
        return new Game
        {
            Id = entity.Id,
            Status = entity.Status,
            Host = getPlayerById(entity.HostId),
            Guest = entity.GuestId.HasValue ? getPlayerById(entity.GuestId.Value) : null,
            Winner = entity.WinnerId.HasValue ? getPlayerById(entity.WinnerId.Value) : null,
            CurrentTurn = entity.CurrentTurnId.HasValue ? getPlayerById(entity.CurrentTurnId.Value) : null,
            Grid = getGridById(entity.GridId)
        };
    }*/

    /*public static GameEntity ToEntity(Game game)
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
    }*/
}