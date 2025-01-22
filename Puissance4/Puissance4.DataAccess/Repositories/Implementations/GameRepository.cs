using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class GameRepository : Repository<EFGame>, IGameRepository
{
    public GameRepository(Puissance4DbContext context) : base(context) { }

    public async Task<EFGame?> GetGameWithGridAsync(int gameId)
    {
        return await _context.Games
            .Include(g => g.Host)
            .Include(g => g.Guest)
            .Include(g => g.Winner)
            .Include(g => g.CurrentTurn)
            .Include(g => g.Grid)
            .ThenInclude(grid => grid.Cells)
            .ThenInclude(cell => cell.Token)
            .FirstOrDefaultAsync(g => g.Id == gameId);
    }

    public async Task<IEnumerable<EFGame>> GetGamesOfPlayerAsync(int playerId)
    {
        return await _context.Games
            .Where(g => g.HostId == playerId || g.GuestId == playerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<EFGame>> GetPendingGames()
    {
        return await _context.Games
            .Where(g => g.Status == EFGame.Statuses.AwaitingGuest)
            .ToListAsync();
    }
}
