using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class GameRepository : IGameRepository
{
    private readonly Puissance4DbContext _context;

    public GameRepository(Puissance4DbContext context)
    {
        _context = context;
    }
    
    public async Task<GameEntity?> GetByIdAsync(int id)
    {
        return await _context.Games.FindAsync(id);
    }

    public async Task<IEnumerable<GameEntity>> GetAllAsync()
    {
        return await _context.Games.ToListAsync();
    }

    public async Task AddAsync(GameEntity game)
    {
        await _context.Games.AddAsync(game);
    }

    public void Update(GameEntity game)
    {
        _context.Games.Update(game);
    }

    public void Delete(GameEntity game)
    {
        _context.Games.Remove(game);
    }

    public async Task<IEnumerable<GameEntity>> GetGamesByStatusAsync(string status)
    {
        return await _context.Games
            .Where(g => g.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<GameEntity>> GetGamesForPlayerAsync(int playerId)
    {
        return await _context.Games
            .Where(g => g.HostId == playerId || g.GuestId == playerId)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
