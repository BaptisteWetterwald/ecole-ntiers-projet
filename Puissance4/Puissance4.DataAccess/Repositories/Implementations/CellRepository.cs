using Microsoft.Data.Sqlite;
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

    public CellEntity GetById(int id)
    {
        CellEntity? cell = _context.Cells.FirstOrDefault(c => c.Id == id);
        if (cell == null) throw new Exception("No cell found with ID " + id);
        return cell;
    }
}