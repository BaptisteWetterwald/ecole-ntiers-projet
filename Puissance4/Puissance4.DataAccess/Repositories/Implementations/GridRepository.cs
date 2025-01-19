using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class GridRepository : IGridRepository
{
    private readonly Puissance4DbContext _context;

    public GridRepository(Puissance4DbContext context)
    {
        _context = context;
    }

    public async Task<GridEntity?> GetByIdAsync(int id)
    {
        return await _context.Grids.FindAsync(id);
    }

    public async Task<IEnumerable<GridEntity>> GetAllAsync()
    {
        return await _context.Grids.ToListAsync();
    }

    public async Task AddAsync(GridEntity grid)
    {
        await _context.Grids.AddAsync(grid);
    }

    public void Update(GridEntity grid)
    {
        _context.Grids.Update(grid);
    }

    public void Delete(GridEntity grid)
    {
        _context.Grids.Remove(grid);
    }

    public async Task<GridEntity?> GetGridByGameIdAsync(int gameId)
    {
        return await _context.Grids
            .FirstOrDefaultAsync(g => g.GameId == gameId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
