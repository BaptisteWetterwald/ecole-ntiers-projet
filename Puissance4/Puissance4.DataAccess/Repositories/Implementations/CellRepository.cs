using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class CellRepository : Repository<EFCell>, ICellRepository
{
    public CellRepository(Puissance4DbContext context) : base(context)
    {
    }

    public async Task<EFCell?> GetCellByCoordinatesAsync(int gridId, int row, int column)
    {
        return await _context.Cells
            .FirstOrDefaultAsync(c => c.Row == row && c.Column == column && c.Id == gridId);
    }
}