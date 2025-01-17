using Blaze4Shared.Models;

public class Player
{
    public Guid Id { get; set; } // Doit être settable pour EF
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!; // Hachage du mot de passe

    public List<Game> Games { get; set; } = new(); // Historique des parties

    // Constructeur par défaut requis pour EF Core
    private Player() { }

    // Constructeur personnalisé
    public Player(string username, string password)
    {
        Id = Guid.NewGuid();
        Username = username;
        PasswordHash = HashPassword(password);
    }

    private string HashPassword(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public bool ValidatePassword(string password)
    {
        return PasswordHash == HashPassword(password);
    }
}