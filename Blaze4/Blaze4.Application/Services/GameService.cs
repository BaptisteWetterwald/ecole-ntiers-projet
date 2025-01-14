using Blaze4.Application.Models;

namespace Blaze4.Application.Services
{
    public class GameService
    {
        private readonly List<Game> _games = new();

        // Créer une nouvelle partie
        public Game CreateGame(Player host)
        {
            var game = new Game
            {
                Host = host,
                Status = Game.AwaitingGuest,
                Grid = new Grid()
            };

            _games.Add(game);
            return game;
        }

        // Récupérer les parties en attente d'invité
        public List<Game> GetGamesAwaitingGuest()
        {
            return _games.Where(g => g.Status == Game.AwaitingGuest).ToList();
        }

        // Permettre à un invité de rejoindre une partie existante
        public Game? JoinGame(Player guest, Guid gameId)
        {
            var game = _games.FirstOrDefault(g => g.Id == gameId);
            if (game == null || game.Guest != null)
                return null;

            game.JoinGame(guest);
            return game;
        }

        // Récupérer les parties auxquelles un joueur a participé
        public List<Game> GetPlayerGames(Player player)
        {
            return _games.Where(g => g.Host == player || g.Guest == player).ToList();
        }

        // Récupérer les parties en cours qui attendent une action du joueur
        public List<Game> GetGamesAwaitingPlayerAction(Player player)
        {
            return _games.Where(g =>
                g.Status == Game.InProgress &&
                ((g.Host == player || g.Guest == player) && 
                (g.IsPlayerTurn(player) && !g.Grid.IsFull()) || !g.CheckWinCondition())).ToList();
        }

        // Gérer un tour de jeu
        public string PlayTurn(Guid gameId, Player player, int column)
        {
            var game = _games.FirstOrDefault(g => g.Id == gameId);
            if (game == null)
                return "Game not found";

            if (!game.IsPlayerTurn(player))
                return "It's not your turn";

            if (game.Grid.IsFull())
                return "The grid is full";

            game.PlayTurn(player, column);

            if (game.Status == Game.Finished)
                return "Game finished";
            
            game.SwitchTurn();
            return "Turn played";
        }
        
        public void StartGame(Guid gameId)
        {
            var game = _games.FirstOrDefault(g => g.Id == gameId);
            if (game == null)
                return;

            game.StartGame();
        }
    }
}
