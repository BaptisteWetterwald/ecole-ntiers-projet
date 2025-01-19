using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class TokenRepository : ITokenRepository
{
    
    private readonly Puissance4DbContext _context;
    
    public TokenRepository(Puissance4DbContext context)
    {
        _context = context;
    }
    
    public TokenEntity GetById(int id)
    {
        return _context.Set<TokenEntity>().FirstOrDefault(t => t.Id == id);
    }

    public void Add(TokenEntity token)
    {
        _context.Set<TokenEntity>().Add(token);
    }
}