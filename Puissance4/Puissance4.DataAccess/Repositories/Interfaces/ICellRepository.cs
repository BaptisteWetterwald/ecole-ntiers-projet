using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess.Repositories.Interfaces;

public interface ICellRepository
{
    CellEntity GetById(int id);
    void SaveChanges();
}