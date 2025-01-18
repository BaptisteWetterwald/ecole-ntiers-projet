using Blaze4.Domain.Models;

namespace Blaze4.Infrastructure.Repositories;

public interface IGameRepository
{
    Task<Game> GetGameAsync(Guid gameId);
    Task AddGameAsync(Game game);
    Task UpdateGameAsync(Game game);
}