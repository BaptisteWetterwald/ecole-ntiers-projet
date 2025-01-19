using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess.Repositories.Interfaces;

public interface IGridRepository : IRepository<GridEntity>
{
    Task<GridEntity?> GetGridByGameIdAsync(int gameId);
}