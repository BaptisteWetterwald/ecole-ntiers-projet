using System.Net.Http.Json;

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

    // Récupérer une partie spécifique
    public async Task<GameDto?> GetGameByIdAsync(int gameId)
    {
        return await _httpClient.GetFromJsonAsync<GameDto>($"games/{gameId}");
    }

    // Créer une nouvelle partie
    public async Task<bool> CreateGameAsync(GameCreateDto game)
    {
        var response = await _httpClient.PostAsJsonAsync("games", game);
        return response.IsSuccessStatusCode;
    }

    // Rejoindre une partie
    public async Task<bool> JoinGameAsync(int gameId, int playerId)
    {
        var response = await _httpClient.PostAsJsonAsync($"games/{gameId}/join", new { PlayerId = playerId });
        return response.IsSuccessStatusCode;
    }
}