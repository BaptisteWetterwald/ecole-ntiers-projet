namespace Blaze4Backend.DTOs;

// Data Transfer Object for a login request
public class LoginRequest
{

    // The login of the user
    public string Login { get; set; }
    
    // The password of the user
    public string Password { get; set; }
    
    // The constructor for the LoginRequest object
    public LoginRequest(string login, string password)
    {
        Login = login;
        Password = password;
    }
}