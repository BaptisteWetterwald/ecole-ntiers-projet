using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess.Repositories.Interfaces;

public interface ITokenRepository
{
    TokenEntity GetById(int id);
    void Add(TokenEntity token);
    void SaveChanges();
}