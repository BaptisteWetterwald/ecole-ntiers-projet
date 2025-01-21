using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class PlayerRepository : Repository<EFPlayer>, IPlayerRepository
{
    public PlayerRepository(Puissance4DbContext context) : base(context)
    {
    }

    public Task<EFPlayer> GetByLoginAsync(string login)
    {
        return _context.Players.FirstOrDefaultAsync(p => p.Login == login);
    }

    public Task<EFPlayer?> GetByLoginAndPasswordAsync(string login, string hashedPassword)
    {
        return _context.Players.FirstOrDefaultAsync(p => p.Login == login && p.PasswordHash == hashedPassword);
    }
}