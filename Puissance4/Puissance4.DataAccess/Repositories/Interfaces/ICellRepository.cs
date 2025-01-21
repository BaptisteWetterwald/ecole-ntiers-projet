using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess.Repositories.Interfaces;

public interface ICellRepository : IRepository<EFCell>
{
    Task<EFCell> GetCellByCoordinatesAsync(int gridId, int row, int column);
}
