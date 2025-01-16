/*
using Blaze4.Application.Models;

namespace Blaze4.Application.Services;

public class PlayerService
{
    
    private readonly List<Player> _players;

    public PlayerService()
    {
        // Liste prédéfinie des joueurs
        _players = new List<Player>
        {
            new Player { Login = "Alice", Password = "password1" },
            new Player { Login = "Bob", Password = "password2" },
        };
    }

    public Player? Authenticate(string login, string password)
    {
        return _players.FirstOrDefault(p => p.Login == login && p.Password == password);
    }

    public List<Player> GetAllPlayers()
    {
        return _players;
    }
}
*/