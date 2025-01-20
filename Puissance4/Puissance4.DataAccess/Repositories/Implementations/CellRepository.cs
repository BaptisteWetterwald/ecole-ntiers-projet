using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class CellRepository : ICellRepository
{
    private readonly Puissance4DbContext _context;

    public CellRepository(Puissance4DbContext context)
    {
        _context = context;
    }

    public async Task<CellEntity?> GetByIdAsync(int id)
    {
        return await _context.Cells
            .Include(c => c.Token) // Inclure le Token pour charger les données liées
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<CellEntity>> GetAllAsync()
    {
        return await _context.Cells
            .Include(c => c.Token) // Inclure les Tokens pour chaque cellule
            .ToListAsync();
    }

    public async Task AddAsync(CellEntity cell)
    {
        await _context.Cells.AddAsync(cell);
    }

    public void Update(CellEntity cell)
    {
        _context.Cells.Update(cell);
    }

    public void Delete(CellEntity cell)
    {
        _context.Cells.Remove(cell);
    }

    public async Task<IEnumerable<CellEntity>> GetCellsByGridIdAsync(int gridId)
    {
        return await _context.Cells
            .Where(c => c.GridId == gridId)
            .Include(c => c.Token) // Inclure les Tokens liés
            .ToListAsync();
    }

    public async Task<CellEntity?> GetCellAt(int gridId, int row, int column)
    {
        return await _context.Cells
            .Include(c => c.Token) // Inclure le Token pour charger les données liées
            .FirstOrDefaultAsync(c => c.GridId == gridId && c.Row == row && c.Column == column);
    }

    public async Task AddCellWithTokenAsync(CellEntity cellEntity, TokenEntity? tokenEntity = null)
    {
        // Ajouter la cellule
        await _context.Cells.AddAsync(cellEntity);
        await _context.SaveChangesAsync(); // Générer l'ID de la cellule

        if (tokenEntity != null)
        {
            // Vérifier si un Token existe déjà pour cette cellule
            var existingToken = await _context.Tokens.FirstOrDefaultAsync(t => t.CellId == cellEntity.Id);

            if (existingToken != null)
            {
                // Mettre à jour le Token existant
                existingToken.Color = tokenEntity.Color;
                _context.Tokens.Update(existingToken);
            }
            else
            {
                // Ajouter un nouveau Token
                tokenEntity.CellId = cellEntity.Id;
                tokenEntity.Id = 0;
                await _context.Tokens.AddAsync(tokenEntity);
            }

            await _context.SaveChangesAsync();
            
            // Mettre à jour la cellule avec le Token
            cellEntity.TokenId = tokenEntity.Id;
            _context.Cells.Update(cellEntity);
            
            await _context.SaveChangesAsync();
        }
    }


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
