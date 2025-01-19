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
}