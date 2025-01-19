using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class CellRepository : ICellRepository
{
    private readonly Puissance4DbContext _context;

    public CellRepository(Puissance4DbContext context)
    {
        _context = context;
    }

    public async Task<CellEntity?> GetByIdAsync(int id)
    {
        return await _context.Cells.FindAsync(id);
    }

    public async Task<IEnumerable<CellEntity>> GetAllAsync()
    {
        return await _context.Cells.ToListAsync();
    }

    public async Task AddAsync(CellEntity cell)
    {
        await _context.Cells.AddAsync(cell);
    }

    public void Update(CellEntity cell)
    {
        _context.Cells.Update(cell);
    }

    public void Delete(CellEntity cell)
    {
        _context.Cells.Remove(cell);
    }

    public async Task<IEnumerable<CellEntity>> GetCellsByGridIdAsync(int gridId)
    {
        return await _context.Cells
            .Where(c => c.GridId == gridId)
            .ToListAsync();
    }

    public async Task<CellEntity?> GetCellAt(int gridId, int row, int column)
    {
        return await _context.Cells
            .FirstOrDefaultAsync(c => c.GridId == gridId && c.Row == row && c.Column == column);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}