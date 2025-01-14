namespace Blaze4.Application.Models;

public class Player
{
    /*
     * class Player {
        +String login
        +String password
        +List<Game> games
    }
     */
    
    public string Login { get; set; } // Identifiant unique du joueur
    public string Password { get; set; } // Mot de passe du joueur (hashé)
    public List<Game> Games { get; set; } = new(); // Parties associées au joueur

}