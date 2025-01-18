namespace Blaze4.Application.Interfaces;

public interface IGameService
{
    Task<Guid> CreateGameAsync(Guid hostId);
    Task JoinGameAsync(Guid gameId, Guid guestId);
    Task PlayMoveAsync(Guid gameId, Guid playerId, int column);
}