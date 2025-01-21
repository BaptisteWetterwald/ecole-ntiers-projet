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


    public async Task<Game> CreateGame()
    {
        throw new NotImplementedException();
    }

    public object? JoinGame(int id, int guestId)
    {
        Console.WriteLine("GameService: JoinGame(" + id + ", " + guestId + ")");
        throw new NotImplementedException();
    }

    public Game PlayTurn(int gameId, int userId, int column)
    {
        /* Faire les repos puis tester ça :
        // Récupérer la partie depuis le repository
        var game = _gameRepository.GetById(gameId);
        if (game == null)
            throw new InvalidOperationException("Game not found.");

        // Récupérer le joueur depuis le repository
        var player = _playerRepository.GetById(userId);
        if (player == null)
            throw new InvalidOperationException("Player not found.");

        // Vérifier les règles du jeu
        if (game.CurrentTurn.Id != player.Id)
            throw new InvalidOperationException("It's not your turn!");

        if (!ValidateMove(column, game))
            throw new InvalidOperationException("Invalid move.");

        // Appliquer le mouvement
        ApplyMoveToGrid(game, player, column);

        // Mettre à jour le tour
        UpdateTurn(game);

        // Sauvegarder les modifications dans la base
        _gameRepository.Update(game);

        return game;
        */
        return null;
    }

    public async Task<Game?> GetGameById(int id)
    {
        var efGame = await _gameRepository.GetGameWithGridAsync(id);
        
        return efGame == null ? null : GameMapper.ToDomain(efGame);
    }
}