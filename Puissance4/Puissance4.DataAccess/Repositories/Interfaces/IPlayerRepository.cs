using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess.Repositories.Interfaces;

public interface IPlayerRepository
{
    void AddPlayer(PlayerEntity player);
    PlayerEntity GetPlayerByLogin(string login);
    void SaveChanges();
}