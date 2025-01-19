using Puissance4.Application.Domain;
using Puissance4.Application.Mappers;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Services;

public class PlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IGameRepository _gameRepository;
    
    public PlayerService(IPlayerRepository playerRepository, IGameRepository gameRepository)
    {
        _playerRepository = playerRepository;
        _gameRepository = gameRepository;
    }
    
    public void AddPlayer(Player player)
    {
        // Mapper le modèle métier en entité
        var playerEntity = PlayerMapper.ToEntity(player);
        _playerRepository.AddPlayer(playerEntity);
    }

    public Player GetPlayerByLogin(string login)
    {
        // Récupérer l'entité depuis le repository
        var playerEntity = _playerRepository.GetPlayerByLogin(login);
        if (playerEntity == null) throw new Exception("Player not found.");

        List<Game> games = _gameRepository.GetGamesForPlayer(playerEntity);
        
        // Mapper l'entité en modèle métier
        return PlayerMapper.ToDomain(playerEntity);
    }

    public Player GetPlayerByLogin(string login)
    {
        var playerEntity = _playerRepository.GetPlayerByLogin(login);
        if (playerEntity == null)
        {
            throw new Exception("Player not found.");
        }
        
        // Mapper le PlayerEntity en Player en récupérant ses jeux via un repository
        return PlayerMapper.ToDomain(
            playerEntity,
            getGamesForPlayer: playerId => _gameRepository.GetGamesForPlayer(playerEntity)
                .Select(gameEntity => GameMapper.ToDomain(gameEntity, mapPlayer: PlayerMapper.ToDomainWithoutGames))
                .ToList()
        );
    }
    
    
}