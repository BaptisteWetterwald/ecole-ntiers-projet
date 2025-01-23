using System.Net.Http.Json;
using Blazored.LocalStorage;
using Puissance4.DTOs;

namespace Puissance4.Presentation.Services;

public class PlayerService
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;

    public PlayerService(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<PlayerDto?> GetPlayerAsync(int id)
    {
        var response = await _httpClient.GetAsync($"players/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PlayerDto>();
    }

    public async Task<PlayerDto> CreatePlayerAsync(string login)
    {
        var response = await _httpClient.PostAsJsonAsync("players", new { Login = login });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PlayerDto>();
    }

    public async Task<BearerTokenDto> LoginAsync(string username, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("players/login", new { Username = username, Password = password });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<BearerTokenDto>();
    }

    public async Task PlayTurnAsync(int gameId, int playerId, int column)
    {
        var response = await _httpClient.PostAsJsonAsync($"games/{gameId}/players/{playerId}/play", new { Column = column });
        response.EnsureSuccessStatusCode();
    }
    
    public async Task<int> GetPlayerIdAsync()
    { 
        // UserId is the same as PlayerId in the token
        var playerIdString = await _authService.GetClaimAsync("UserId") as string;

        if (string.IsNullOrEmpty(playerIdString))
        {
            throw new Exception("PlayerId not found in token");
        }

        if (!int.TryParse(playerIdString, out int playerId))
        {
            throw new Exception("Invalid PlayerId format in token");
        }

        return playerId;
    }

}