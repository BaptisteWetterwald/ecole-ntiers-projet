using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class GridRepository : Repository<EFGrid>, IGridRepository
{
    public GridRepository(Puissance4DbContext context) : base(context) { }

    public async Task<EFGrid?> GetGridWithCellsAsync(int gridId)
    {
        return await _context.Grids
            .Include(g => g.Cells)
            .FirstOrDefaultAsync(g => g.Id == gridId);
    }
}
