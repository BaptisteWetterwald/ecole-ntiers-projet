namespace Blaze4.Application.Models;

public class Player
{
    public string Login { get; }
    public string Password { get; }
    public List<Game> Games { get; } = new();

    public Player(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public bool ValidateCredentials(string login, string password)
    {
        return Login == login && Password == password;
    }
}