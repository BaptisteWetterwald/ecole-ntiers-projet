using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace Puissance4.Presentation.Services;

using System.Net.Http.Json;
using Blazored.LocalStorage;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<bool> Login(string username, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login", new { Username = username, Password = password });
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
}
