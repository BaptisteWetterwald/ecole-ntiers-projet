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