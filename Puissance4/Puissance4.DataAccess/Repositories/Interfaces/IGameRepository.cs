using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess.Repositories.Interfaces;

public interface IGameRepository : IRepository<EFGame>
{
    Task<EFGame?> GetGameWithGridAsync(int gameId);
    Task<IEnumerable<EFGame>> GetGamesByPlayerAsync(int playerId);
}
