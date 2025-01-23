using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;

namespace Puissance4.Presentation.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly JwtAuthenticationStateProvider _jwtAuthenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage,
        JwtAuthenticationStateProvider jwtAuthenticationStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _jwtAuthenticationStateProvider = jwtAuthenticationStateProvider;
    }

    public async Task<bool> Login(string username, string password)
    {
        var response =
            await _httpClient.PostAsJsonAsync("auth/login", new { Username = username, Password = password });
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<Dictionary<string, string>>(content)?["token"];

            if (string.IsNullOrEmpty(token)) throw new Exception("Invalid token format");

            await _localStorage.SetItemAsync("authToken", token);
            return true;
        }

        return false;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
    }

    public async Task<bool> IsAuthenticated()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        return !string.IsNullOrEmpty(token);
    }

    public async Task<bool> Register(string username, string password)
    {
        var response =
            await _httpClient.PostAsJsonAsync("auth/register", new { Username = username, Password = password });
        return response.IsSuccessStatusCode;
    }

    public async Task<object?> GetClaimAsync(string claim)
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (string.IsNullOrEmpty(token)) return string.Empty;

        var claims = _jwtAuthenticationStateProvider.ParseClaimsFromJwt(token);
        return claims.FirstOrDefault(c => c.Type == claim)?.Value;
    }
}