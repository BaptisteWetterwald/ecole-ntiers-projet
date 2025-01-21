namespace Puissance4.DataAccess.Entities;

public class EFPlayer
{
    public int Id { get; set; } // Clé primaire
    public string Login { get; set; } // Nom d'utilisateur
    public string PasswordHash { get; set; } // Hash du mot de passe

    // Jeux où le joueur est hôte
    public ICollection<EFGame> GamesAsHost { get; set; }
    
    // Jeux où le joueur est invité
    public ICollection<EFGame> GamesAsGuest { get; set; }
}
