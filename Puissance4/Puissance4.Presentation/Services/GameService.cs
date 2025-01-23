using System.Net.Http.Json;
using Puissance4.DTOs;

namespace Puissance4.Presentation.Services;

public class GameService
{
    private readonly HttpClient _httpClient;

    public GameService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Récupérer la liste des parties
    public async Task<List<GameDto>> GetAllGamesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<GameDto>>("games")
               ?? new List<GameDto>();
    }

    public async Task<List<GameDto>> GetPendingGamesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<GameDto>>("games/pending")
               ?? new List<GameDto>();
    }

    public async Task<List<GameDto>> GetGamesOfPlayerAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<GameDto>>("games/player")
               ?? new List<GameDto>();
    }

    // Récupérer une partie spécifique
    public async Task<GameDto?> GetGameByIdAsync(int gameId)
    {
        return await _httpClient.GetFromJsonAsync<GameDto>($"games/{gameId}");
    }

    // Créer une nouvelle partie
    public async Task<bool> CreateGameAsync()
    {
        var response = await _httpClient.PostAsync("games/create", null);
        return response.IsSuccessStatusCode;
    }

    // Rejoindre une partie
    public async Task<bool> JoinGameAsync(int gameId)
    {
        var response = await _httpClient.PostAsync($"games/{gameId}/join", null);
        return response.IsSuccessStatusCode;
    }

    // Jouer un coup
    public async Task<bool> PlayMoveAsync(int gameId, int column)
    {
        var playTurnDto = new PlayTurnDto { Column = column };
        var response = await _httpClient.PostAsJsonAsync($"games/{gameId}/play", playTurnDto);
        return response.IsSuccessStatusCode;
    }
}