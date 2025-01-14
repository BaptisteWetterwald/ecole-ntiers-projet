namespace Blaze4.Application.Models;

public class Game
{
    /*
     * class Game {
        +Player host
        +Player guest
        +Grid grid
        +String status
        +startGame()
        +joinGame(Player guest)
        +playTurn(Player player, int column)
        +checkWinCondition(): boolean
    }
     */
    public Guid Id { get; private set; } = Guid.NewGuid(); // Identifiant de la partie
    public Player Host { get; set; } // Joueur hôte
    public Player? Guest { get; set; } // Joueur invité
    public Grid Grid { get; set; } = new(); // Grille de jeu
    public string Status { get; set; } = "Awaiting Guest"; // Statut de la partie ("Awaiting Guest", "In Progress", "Finished")

    private Player? _currentTurn; // Joueur dont c'est le tour

    // Méthode pour démarrer une partie
    public void StartGame()
    {
        if (Guest == null) throw new InvalidOperationException("A guest must join before starting the game.");
        Status = "In Progress";
        _currentTurn = Host; // Le joueur hôte commence
    }

    // Méthode pour rejoindre une partie
    public void JoinGame(Player guest)
    {
        if (Guest != null) throw new InvalidOperationException("This game already has a guest.");
        Guest = guest;
    }

    // Méthode pour vérifier si c'est le tour du joueur
    public bool IsPlayerTurn(Player player)
    {
        return _currentTurn == player;
    }

    // Méthode pour changer de tour
    public void SwitchTurn()
    {
        _currentTurn = _currentTurn == Host ? Guest : Host;
    }

    // Méthode pour jouer un tour
    public void PlayTurn(Player player, int column)
    {
        if (Status != "In Progress") throw new InvalidOperationException("The game is not in progress.");
        if (Grid.IsFull()) throw new InvalidOperationException("The grid is full.");
        if (player != Host && player != Guest) throw new UnauthorizedAccessException("Only participants can play.");
        if (player != _currentTurn) throw new InvalidOperationException("It's not your turn."); // Vérifie si c'est bien le tour du joueur

        if (Grid.DropToken(column, new Token { Color = player == Host ? "Red" : "Yellow" }))
        {
            if (CheckWinCondition())
            {
                Status = "Finished"; // La partie est terminée si un joueur gagne
            }
            else
            {
                SwitchTurn(); // Change le tour du joueur
            }
        }
    }

    // Méthode pour vérifier les conditions de victoire
    public bool CheckWinCondition()
    {
        return Grid.CheckWin();
    }
}
