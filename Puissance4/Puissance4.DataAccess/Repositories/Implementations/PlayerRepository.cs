using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class PlayerRepository : IPlayerRepository
{
    private readonly Puissance4DbContext _context;
    
    public PlayerRepository(Puissance4DbContext context)
    {
        _context = context;
    }
    
    public void AddPlayer(PlayerEntity player)
    {
        
    }

    public PlayerEntity GetPlayerByLogin(string login)
    {
        throw new NotImplementedException();
    }
}