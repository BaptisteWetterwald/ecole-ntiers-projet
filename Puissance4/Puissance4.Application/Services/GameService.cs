using Microsoft.AspNetCore.Mvc;
using Puissance4.Application.Domain;
using Puissance4.Application.DTOs;
using Puissance4.Application.Mappers;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Services;

public class GameService
{
    private readonly IGameRepository _gameRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IGridRepository _gridRepository;
    private readonly ICellRepository _cellRepository;

    public GameService(
        IGameRepository gameRepository,
        IPlayerRepository playerRepository,
        IGridRepository gridRepository,
        ICellRepository cellRepository
    )
    {
        _gameRepository = gameRepository;
        _playerRepository = playerRepository;
        _gridRepository = gridRepository;
        _cellRepository = cellRepository;
    }


    public async Task<GameDto> CreateGame(int hostId)
    {
        Console.WriteLine("GameService: CreateGame(" + hostId + ")");
        
        var host = await _playerRepository.GetByIdAsync(hostId);
        if (host == null)
        {
            throw new ArgumentException("Player not found");
        }

        var game = new Game(PlayerMapper.ToDomain(host));
        var efGame = GameMapper.ToEntity(game);
        await _gameRepository.AddAsync(efGame);
        
        return GameMapper.ToDto(efGame); // Using efGame instead of game to get the Id from the database
    }

    public async Task<GameDto> JoinGame(int gameId, int guestId)
    {
        Console.WriteLine("GameService: JoinGame(" + gameId + ", " + guestId + ")");
        
        var efGame = await _gameRepository.GetGameWithGridAsync(gameId);
        if (efGame == null)
        {
            throw new ArgumentException("Game not found");
        }
        
        var guest = await _playerRepository.GetByIdAsync(guestId);
        if (guest == null)
        {
            throw new ArgumentException("Player not found");
        }
        
        if (efGame.HostId == guestId)
        {
            throw new ArgumentException("Host cannot join his own game");
        }
        
        var gameDomain = GameMapper.ToDomain(efGame);
        gameDomain.JoinGame(PlayerMapper.ToDomain(guest));
        
        efGame.GuestId = gameDomain.Guest?.Id;
        efGame.CurrentTurnId = gameDomain.CurrentTurn?.Id;
        efGame.Status = gameDomain.Status;
        
        await _gameRepository.UpdateAsync(efGame);
        
        return GameMapper.ToDto(efGame);
    }

    public async Task<GameDto> PlayTurn(int gameId, int playerId, int column)
    {
        Console.WriteLine($"GameService: PlayTurn({gameId}, {playerId}, {column})");

        // Charger le jeu et ses relations
        var efGame = await _gameRepository.GetGameWithGridAsync(gameId);
        if (efGame == null)
        {
            throw new ArgumentException("Game not found");
        }
        
        if (efGame.Status != EFGame.Statuses.InProgress)
        {
            throw new InvalidOperationException("Game is not in progress");
        }

        if (efGame.Grid == null)
        {
            throw new InvalidOperationException("Grid is not initialized for this game.");
        }

        var efPlayer = await _playerRepository.GetByIdAsync(playerId);
        if (efPlayer == null)
        {
            throw new ArgumentException("Player not found");
        }

        // Mapper EFGame vers Domain Model
        var gameDomain = GameMapper.ToDomain(efGame);

        // Logique métier : jouer un tour
        gameDomain.PlayTurn(PlayerMapper.ToDomain(efPlayer), column);

        // Synchroniser les cellules modifiées ou ajoutées
        foreach (var domainCell in gameDomain.Grid.Cells)
        {
            if (domainCell.Token != null) // Seules les cellules avec un token sont traitées
            {
                var existingCell = efGame.Grid.Cells
                    .FirstOrDefault(c => c.Row == domainCell.Row && c.Column == domainCell.Column);

                if (existingCell == null)
                {
                    // Ajouter une nouvelle cellule avec un token
                    var newCell = CellMapper.ToEntity(domainCell, efGame.Id);
                    efGame.Grid.Cells.Add(newCell);
                }
                else
                {
                    // Mettre à jour la cellule existante
                    existingCell.TokenColor = domainCell.Token.Color;
                }
            }
        }

        // Synchroniser les propriétés principales de EFGame
        efGame.WinnerId = gameDomain.Winner?.Id;
        efGame.CurrentTurnId = gameDomain.CurrentTurn?.Id;
        efGame.Status = gameDomain.Status;

        // Mettre à jour l'entité principale
        await _gameRepository.UpdateAsync(efGame);

        return GameMapper.ToDto(efGame);
    }


    public async Task<GameDto?> GetGameById(int id)
    {
        Console.WriteLine("GameService: GetGameById(" + id + ")");
        
        var efGame = await _gameRepository.GetGameWithGridAsync(id);
        return efGame == null ? null : GameMapper.ToDto(efGame);
    }

    public async Task<IEnumerable<GameDto>> GetPendingGames()
    {
        Console.WriteLine("GameService: GetPendingGames()");
        
        var efGames = await _gameRepository.GetPendingGames();

        var gamesAsList = efGames.ToList().Select(g => new { g.Id });
        // This contains the games IDs but not the inner objects
        
        var dtos = new List<GameDto>();
        // call _gameRepository.GetGameWithGridAsync(gameId) for each gameId in gamesAsList, then convert to DTO
        foreach (var game in gamesAsList)
        {
            var efGame = await _gameRepository.GetGameWithGridAsync(game.Id);
            if (efGame != null) dtos.Add(GameMapper.ToDto(efGame));
        }
        
        return dtos;
    }

    public async Task<IEnumerable<GameDto>> GetGamesOfPlayer(int playerId)
    {
        Console.WriteLine("GameService: GetGamesOfPlayer(" + playerId + ")");
        
        var efGames = await _gameRepository.GetGamesOfPlayerAsync(playerId);
        // This contains the games IDs but not the inner objects
        
        var dtos = new List<GameDto>();
        // call _gameRepository.GetGameWithGridAsync(gameId) for each gameId in gamesAsList, then convert to DTO
        foreach (var efGame in efGames)
        {
            var game = await _gameRepository.GetGameWithGridAsync(efGame.Id);
            if (game != null) dtos.Add(GameMapper.ToDto(game));
        }
        
        return dtos;
    }
}