/*
using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class PlayerRepository : IPlayerRepository
{
    private readonly Puissance4DbContext _context;

    public PlayerRepository(Puissance4DbContext context)
    {
        _context = context;
    }

    public async Task<PlayerEntity?> GetByIdAsync(int id)
    {
        return await _context.Players.FindAsync(id);
    }

    public async Task<IEnumerable<PlayerEntity>> GetAllAsync()
    {
        return await _context.Players.ToListAsync();
    }

    public async Task AddAsync(PlayerEntity player)
    {
        await _context.Players.AddAsync(player);
    }

    public void Update(PlayerEntity player)
    {
        _context.Players.Update(player);
    }

    public void Delete(PlayerEntity player)
    {
        _context.Players.Remove(player);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<PlayerEntity?> GetByLoginAsync(string login)
    {
        return await _context.Players
            .FirstOrDefaultAsync(p => p.Login == login);
    }

    public async Task<IEnumerable<GameEntity>> GetPlayerGamesAsync(int playerId)
    {
        return await _context.Games
            .Where(g => g.HostId == playerId || g.GuestId == playerId)
            .ToListAsync();
    }
}
*/