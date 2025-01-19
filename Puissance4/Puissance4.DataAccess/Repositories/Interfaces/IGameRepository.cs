using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess.Repositories.Interfaces;

public interface IGameRepository : IRepository<GameEntity>
{
    Task<IEnumerable<GameEntity>> GetGamesByStatusAsync(string status);
    Task<IEnumerable<GameEntity>> GetGamesForPlayerAsync(int playerId);
}
