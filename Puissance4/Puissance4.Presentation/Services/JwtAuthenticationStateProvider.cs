using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Puissance4.Presentation.Services;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AuthenticationState _anonymous;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILocalStorageService _localStorage;

    public JwtAuthenticationStateProvider(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
    {
        _localStorage = localStorage;
        _httpClientFactory = httpClientFactory;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Récupère le token depuis le stockage local
        var token = await _localStorage.GetItemAsync<string>("authToken");

        // Si aucun token n'existe, retourne un utilisateur non authentifié
        if (string.IsNullOrWhiteSpace(token)) return _anonymous;

        // Décode le token JWT et crée un ClaimsPrincipal
        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public void MarkUserAsAuthenticated(string token)
    {
        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void MarkUserAsLoggedOut()
    {
        _localStorage.RemoveItemAsync("authToken");
        NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
    }

    public IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = Convert.FromBase64String(PadBase64(payload));
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? ""));
    }

    private string PadBase64(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: return base64 + "==";
            case 3: return base64 + "=";
            default: return base64;
        }
    }
}