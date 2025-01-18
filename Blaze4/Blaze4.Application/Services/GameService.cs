using Blaze4.Application.Interfaces;

namespace Blaze4.Application.Services;

using Blaze4.Domain.Models;
using Blaze4.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class GameService : IGameService
{
    private readonly Blaze4DbContext _dbContext;

    public GameService(Blaze4DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateGameAsync(Guid hostId)
    {
        var host = await _dbContext.Players.FindAsync(hostId);
        if (host == null) throw new Exception("Host player not found.");

        var game = new Game(host);
        await _dbContext.Games.AddAsync(game);
        await _dbContext.SaveChangesAsync();

        return game.Id;
    }

    public async Task JoinGameAsync(Guid gameId, Guid guestId)
    {
        var game = await _dbContext.Games
            .Include(g => g.Guest)
            .FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null) throw new Exception("Game not found.");

        var guest = await _dbContext.Players.FindAsync(guestId);
        if (guest == null) throw new Exception("Guest player not found.");

        game.JoinGame(guest);
        _dbContext.Games.Update(game);
        await _dbContext.SaveChangesAsync();
    }

    public async Task PlayMoveAsync(Guid gameId, Guid playerId, int column)
    {
        var game = await _dbContext.Games
            .Include(g => g.Grid)
            .ThenInclude(grid => grid.Cells)
            .FirstOrDefaultAsync(g => g.Id == gameId);

        if (game == null) throw new Exception("Game not found.");
        var player = await _dbContext.Players.FindAsync(playerId);
        if (player == null) throw new Exception("Player not found.");

        game.PlayTurn(player, column);

        _dbContext.Games.Update(game);
        await _dbContext.SaveChangesAsync();
    }
}
