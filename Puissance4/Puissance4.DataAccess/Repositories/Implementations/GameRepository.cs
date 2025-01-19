/*
 using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class GameRepository : IGameRepository
{
    private readonly Puissance4DbContext _context;

    public GameRepository(Puissance4DbContext context)
    {
        _context = context;
    }

    public GameEntity GetGameById(int id)
    {
        var game = _context.Games.FirstOrDefault(g => g.Id == id);
        if (game == null) throw new Exception("No game found with ID " + id);
        return game;
    }

    public List<GameEntity> GetGamesForPlayer(PlayerEntity player)
    {
        var games = _context.Games.Where(g => g.HostId == player.Id || g.GuestId == player.Id).ToList();
        return games;
    }

    public void SaveGame(GameEntity game)
    {
        var hostEntity = _context.Players.Find(game.HostId);
        var guestEntity = _context.Players.Find(game.GuestId);
        if (hostEntity == null) throw new Exception("No host found for game " + game);
        _context.Games.Add(game);
        hostEntity.Games.Add(game);
        guestEntity?.Games.Add(game);
    }

    public void DeleteGame(int id)
    {
        var game = _context.Games.Find(id);
        if (game == null) throw new Exception("No game found with ID " + id);
        _context.Games.Remove(game);
    }
}
*/

using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess.Repositories.Implementations;

public class GameRepository : IGameRepository
{
    private readonly Puissance4DbContext _context;

    public GameRepository(Puissance4DbContext context)
    {
        _context = context;
    }

    // Récupérer une partie par son ID
    public GameEntity GetGameById(int id)
    {
        return _context.Games
            .Include(g => g.Host) // Inclure l'hôte
            .Include(g => g.Guest) // Inclure l'invité
            .Include(g => g.Grid) // Inclure la grille
            .ThenInclude(grid => grid.Cells) // Inclure les cellules de la grille
            .FirstOrDefault(g => g.Id == id);
    }

    // Récupérer toutes les parties pour un joueur donné
    public IEnumerable<GameEntity> GetGamesForPlayer(int playerId)
    {
        return _context.Games
            .Where(g => g.HostId == playerId || g.GuestId == playerId)
            .Include(g => g.Host)
            .Include(g => g.Guest)
            .Include(g => g.Grid);
    }

    // Ajouter une nouvelle partie
    public void Add(GameEntity game)
    {
        _context.Games.Add(game);
    }

    // Mettre à jour une partie existante
    public void Update(GameEntity game)
    {
        _context.Games.Update(game);
    }

    // Sauvegarder les modifications dans la base
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
