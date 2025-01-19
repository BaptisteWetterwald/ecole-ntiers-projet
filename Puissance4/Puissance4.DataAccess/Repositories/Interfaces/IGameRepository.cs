/*using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess.Repositories.Interfaces;

public interface IGameRepository
{
    GameEntity GetGameById(int id);
    List<GameEntity> GetGamesForPlayer(PlayerEntity player);
    void SaveGame(GameEntity game);
    void DeleteGame(int id);
}*/
using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess.Repositories.Interfaces;

public interface IGameRepository
{
    GameEntity GetGameById(int id);
    IEnumerable<GameEntity> GetGamesForPlayer(int playerId);
    void Add(GameEntity game);
    void Update(GameEntity game);
    void SaveChanges();
}
