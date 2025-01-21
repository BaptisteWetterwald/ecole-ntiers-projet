using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class GridRepository : IGridRepository
{
    private readonly Puissance4DbContext _context;
    private readonly ICellRepository _cellRepository;

    public GridRepository(Puissance4DbContext context, ICellRepository cellRepository)
    {
        _context = context;
        _cellRepository = cellRepository;
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
        _context.SaveChanges();
    }

    public async Task<GridEntity?> GetGridByGameIdAsync(int gameId)
    {
        return await _context.Grids
            .FirstOrDefaultAsync(g => g.GameId == gameId);
    }

    public async Task AddGridWithCellsAsync(GridEntity gridEntity)
    {
        await _context.Grids.AddAsync(gridEntity);
        await _context.SaveChangesAsync();

        Console.WriteLine("Debug AddGridWithCellsAsync: gridEntity.Cells.Count = " + gridEntity.Cells.Count);

        for (int row = 0; row < gridEntity.Rows; row++)
        {
            for (int column = 0; column < gridEntity.Columns; column++)
            {
                var cell = new CellEntity
                {
                    GridId = gridEntity.Id,
                    Row = row,
                    Column = column
                };
                
                await _cellRepository.AddAsync(cell);
            }
        }
        
        await _context.SaveChangesAsync();
    }
}