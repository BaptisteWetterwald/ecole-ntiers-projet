namespace Puissance4.Application.Domain;

public class Player
{
    public int Id { get; set; }
    public string Login { get; set; } // Nom d'utilisateur unique
    public string PasswordHash { get; set; } // Hachage du mot de passe
    public List<Game> Games { get; set; } = new(); // Parties auxquelles il joue actuellement
    
    public Player(string login, string password)
    {
        Login = login;
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
    
    public void AddGame(Game game)
    {
        Games.Add(game);
    }
    
    public void RemoveGame(Game game)
    {
        Games.Remove(game);
    }
    
    public void ClearGames()
    {
        Games.Clear();
    }
    
}