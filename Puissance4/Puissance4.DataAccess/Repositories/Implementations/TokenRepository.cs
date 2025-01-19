using Microsoft.EntityFrameworkCore;
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
    
    public async Task<TokenEntity?> GetByIdAsync(int id)
    {
        return await _context.Tokens.FindAsync(id);
    }

    public async Task<IEnumerable<TokenEntity>> GetAllAsync()
    {
        return await _context.Tokens.ToListAsync();
    }

    public async Task AddAsync(TokenEntity token)
    {
        await _context.Tokens.AddAsync(token);
    }

    public void Update(TokenEntity token)
    {
        _context.Tokens.Update(token);
    }

    public void Delete(TokenEntity token)
    {
        _context.Tokens.Remove(token);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}