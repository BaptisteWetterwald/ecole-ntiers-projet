using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess.Repositories.Interfaces;

public interface ICellRepository : IRepository<CellEntity>
{
    Task<IEnumerable<CellEntity>> GetCellsByGridIdAsync(int gridId);
    Task<CellEntity?> GetCellAt(int gridId, int row, int column);
}