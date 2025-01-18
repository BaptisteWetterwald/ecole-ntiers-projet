namespace Blaze4.Infrastructure.Hubs;

public class GameHub : Hub
{
    private readonly IGameService _gameService;

    public GameHub(IGameService gameService)
    {
        _gameService = gameService;
    }

    public async Task CreateGameAsync(Guid hostId)
    {
        var gameId = await _gameService.CreateGameAsync(hostId);
        await Clients.Caller.SendAsync("GameCreated", gameId);
    }

    public async Task JoinGameAsync(Guid gameId, Guid guestId)
    {
        await _gameService.JoinGameAsync(gameId, guestId);
        await Clients.Group(gameId.ToString()).SendAsync("GameJoined", guestId);
    }

    public async Task PlayMoveAsync(Guid gameId, Guid playerId, int column)
    {
        await _gameService.PlayMoveAsync(gameId, playerId, column);
        await Clients.Group(gameId.ToString()).SendAsync("MovePlayed", playerId, column);
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        await Clients.Caller.SendAsync("Connected", Context.ConnectionId);
    }

    public async Task JoinGameGroupAsync(Guid gameId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
    }

    public async Task LeaveGameGroupAsync(Guid gameId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
    }
}