using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess.Repositories.Interfaces;

public interface IPlayerRepository : IRepository<PlayerEntity>
{
    Task<PlayerEntity?> GetByLoginAsync(string login);
    Task<IEnumerable<GameEntity>> GetPlayerGamesAsync(int playerId);
}