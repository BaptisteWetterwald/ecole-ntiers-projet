using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess.Repositories.Interfaces;

public interface IPlayerRepository : IRepository<EFPlayer>
{
    Task<EFPlayer?> GetByLoginAsync(string login);
    Task<EFPlayer?> GetByLoginAndPasswordAsync(string login, string hashedPassword);
}