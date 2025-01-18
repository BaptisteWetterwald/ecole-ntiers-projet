namespace Blaze4.Domain.Models;

public class Player
{
    public Guid Id { get; private set; } // Clé unique pour la base de données
    public string Username { get; private set; }
    public string PasswordHash { get; private set; } // Hachage du mot de passe
    public string Role { get; private set; } = "Player"; // Rôle par défaut
    public List<Game> Games { get; private set; } = new(); // Historique des parties

    public Player(string username, string password)
    {
        Id = Guid.NewGuid();
        Username = username;
        PasswordHash = HashPassword(password);
    }

    private string HashPassword(string password)
    {
        // Implémentation simplifiée pour le hachage
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public bool ValidatePassword(string password)
    {
        // Compare le mot de passe fourni avec le hachage
        return PasswordHash == HashPassword(password);
    }

    public void UpdatePassword(string newPassword)
    {
        PasswordHash = HashPassword(newPassword);
    }
}