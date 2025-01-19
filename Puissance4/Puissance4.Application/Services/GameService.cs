using Puissance4.Application.Domain;
using Puissance4.Application.Mappers;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Services;

public class GameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IGridRepository _gridRepository;

    public GameService(
        IGameRepository gameRepository,
        IPlayerRepository playerRepository,
        IGridRepository gridRepository)
    {
        _gameRepository = gameRepository;
        _playerRepository = playerRepository;
        _gridRepository = gridRepository;
    }
    
    public Game GetGameById(int id)
    {
        var gameEntity = _gameRepository.GetGameById(id);
        if (gameEntity == null)
        {
            throw new Exception("Game not found.");
        }

        return GameMapper.ToDomain(
            gameEntity,
            mapPlayer: playerEntity => PlayerMapper.ToDomainWithoutGames(playerEntity),
            grid: GridMapper.ToDomain(gameEntity.Grid)
        );
    }

    // Créer une nouvelle partie
    public Game CreateGame(int hostId)
    {
        var host = _playerRepository.GetPlayerById(hostId);
        if (host == null)
        {
            throw new Exception("Host not found.");
        }

        // Initialiser une grille vide (6x7)
        var grid = new Grid(6, 7);

        var game = new Game
        {
            Status = "Awaiting Guest",
            Host = PlayerMapper.ToDomainWithoutGames(host),
            Grid = grid
        };

        var gameEntity = GameMapper.ToEntity(game);
        _gameRepository.Add(gameEntity);
        _gameRepository.SaveChanges();

        return game;
    }

    // Rejoindre une partie existante
    public Game JoinGame(int gameId, int guestId)
    {
        var gameEntity = _gameRepository.GetGameById(gameId);
        if (gameEntity == null || gameEntity.Status != "Awaiting Guest")
        {
            throw new Exception("Game is not available to join.");
        }

        var guest = _playerRepository.GetPlayerById(guestId);
        if (guest == null)
        {
            throw new Exception("Guest not found.");
        }

        // Mettre à jour la partie avec l'invité
        gameEntity.GuestId = guest.Id;
        gameEntity.Status = "In Progress";
        gameEntity.CurrentTurnId = gameEntity.HostId; // Commence par l'hôte
        _gameRepository.SaveChanges();

        return GameMapper.ToDomain(
            gameEntity,
            mapPlayer: playerEntity => PlayerMapper.ToDomainWithoutGames(playerEntity),
            grid: GridMapper.ToDomain(gameEntity.Grid)
        );
    }

    // Effectuer un tour de jeu
    public Game PlayTurn(int gameId, int playerId, int column)
    {
        var gameEntity = _gameRepository.GetGameById(gameId);
        if (gameEntity == null || gameEntity.Status != "In Progress")
        {
            throw new Exception("Game is not in progress.");
        }

        var game = GameMapper.ToDomain(
            gameEntity,
            mapPlayer: playerEntity => PlayerMapper.ToDomainWithoutGames(playerEntity),
            grid: GridMapper.ToDomain(gameEntity.Grid)
        );

        if (game.CurrentTurn.Id != playerId)
        {
            throw new Exception("It's not your turn.");
        }

        // Déterminer la couleur du jeton en fonction du joueur actuel
        var tokenColor = game.CurrentTurn.Id == game.Host.Id ? "Red" : "Yellow";

        // Jouer le tour
        game.Grid.DropToken(column, tokenColor);

        // Vérifier les conditions de fin de partie
        if (game.Grid.HasWinner())
        {
            game.Status = "Finished";
            game.Winner = game.CurrentTurn;
        }
        else if (game.Grid.IsFull())
        {
            game.Status = "Finished"; // Partie terminée, match nul
        }
        else
        {
            // Passer au joueur suivant
            game.CurrentTurn = game.CurrentTurn.Id == game.Host.Id ? game.Guest : game.Host;
        }

        // Mettre à jour la partie en base de données
        var updatedGameEntity = GameMapper.ToEntity(game);
        _gameRepository.Update(updatedGameEntity);
        _gameRepository.SaveChanges();

        return game;
    }
}


/*
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

    /*public Game GetGameById(int id)
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
    }*/
/*
    public Game GetGameById(int id)
    {
        // Récupérer le GameEntity
        GameEntity gameEntity = _gameRepository.GetGameById(id);
        if (gameEntity == null)
        {
            throw new Exception("Game not found.");
        }

        // Mapper les Players sans inclure leurs listes de jeux
        Func<PlayerEntity, Player> mapPlayer = playerEntity => 
            PlayerMapper.ToDomain(playerEntity, includeGames: false);

        // Mapper le GameEntity en Game
        return GameMapper.ToDomain(gameEntity, mapPlayer);
    }

}*/

/*using Puissance4.Application.Domain;
using Puissance4.Application.Mappers;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Services;

public class GameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IGridRepository _gridRepository;

    public GameService(
        IGameRepository gameRepository,
        IPlayerRepository playerRepository,
        IGridRepository gridRepository)
    {
        _gameRepository = gameRepository;
        _playerRepository = playerRepository;
        _gridRepository = gridRepository;
    }

    public Game GetGameById(int id)
    {
        // Récupérer le GameEntity
        GameEntity gameEntity = _gameRepository.GetGameById(id);
        if (gameEntity == null)
        {
            throw new Exception("Game not found."); // Tu peux remplacer par une exception métier spécifique
        }

        // Récupérer les relations nécessaires
        var host = _playerRepository.GetPlayerById(gameEntity.HostId);
        var guest = gameEntity.GuestId.HasValue ? _playerRepository.GetPlayerById(gameEntity.GuestId.Value) : null;
        var winner = gameEntity.WinnerId.HasValue ? _playerRepository.GetPlayerById(gameEntity.WinnerId.Value) : null;
        var currentTurn = gameEntity.CurrentTurnId.HasValue ? _playerRepository.GetPlayerById(gameEntity.CurrentTurnId.Value) : null;
        var gridEntity = _gridRepository.GetGridById(gameEntity.GridId);

        // Mapper les entités en modèles métier
        return GameMapper.ToDomain(
            gameEntity,
            mapPlayer: playerEntity => PlayerMapper.ToDomain(playerEntity, includeGames: false),
            grid: GridMapper.ToDomain(gridEntity)
        );
    }

    public void SaveGame(Game game)
    {
        // Mapper le modèle métier Game en entité GameEntity
        var gameEntity = GameMapper.ToEntity(game);

        // Sauvegarder le GameEntity dans le repository
        _gameRepository.SaveGame(gameEntity);
    }
}
*/