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
            // Associer le Token à la cellule
            tokenEntity.CellId = cellEntity.Id;

            // Ajouter ou mettre à jour le Token
            var existingToken = await _context.Tokens.FirstOrDefaultAsync(t => t.CellId == cellEntity.Id);
            if (existingToken != null)
            {
                existingToken.Color = tokenEntity.Color;
            }
            else
            {
                await _context.Tokens.AddAsync(tokenEntity);
            }

            await _context.SaveChangesAsync();

            // Mettre à jour la cellule avec le Token
            cellEntity.TokenId = tokenEntity.Id;
            _context.Update(cellEntity);

            await _context.SaveChangesAsync();
        }
    }

    public async Task AddCellWithTokenAsync(CellEntity cellEntity)
    {
        // Extraire le token s'il existe
        var tokenEntity = cellEntity.Token;
        cellEntity.Token = null; // Retirer la référence circulaire avant de passer à l'autre méthode

        // Appeler la méthode principale
        await AddCellWithTokenAsync(cellEntity, tokenEntity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
