using Puissance4.Application.Domain;
using Puissance4.Application.Mappers;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Services;

public class GameService
{
    private readonly IGameRepository _gameRepository;
    private readonly TokenService _tokenService;
    private readonly PlayerService _playerService;

    public GameService(IGameRepository gameRepository, TokenService tokenService, PlayerService playerService)
    {
        _gameRepository = gameRepository;
        _tokenService = tokenService;
        _playerService = playerService;
    }

    public Game GetGameById(int id)
    {
        // Récupérer l'entité Cell depuis le repository
        GameEntity gameEntity = _gameRepository.GetGameById(id);
        var gamesEntities = _gameRepository.GetGamesForPlayer(gameEntity.Host);
        List<Game> games = new List<Game>();
        foreach (var entity in gamesEntities)
        {
            GameMapper.ToDomain(entity);
        }
        Player host = PlayerMapper.ToDomain(gameEntity.Host);
        
        return GameMapper.ToDomain(gameEntity);

        // Récupérer le Token correspondant (si TokenId n'est pas nul)
        Token? token = cellEntity.TokenId.HasValue ? _tokenService.GetTokenById(cellEntity.TokenId.Value) : null;
        
        // Mapper l'entité en modèle métier
        return CellMapper.ToDomain(cellEntity, token);
    }
}