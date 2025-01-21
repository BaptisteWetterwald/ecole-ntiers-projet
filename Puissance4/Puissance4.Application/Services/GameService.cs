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

    public async Task Test()
    {
        Console.WriteLine("GameService: Test()");

        var grid = new EFGrid
        {
            Rows = 6,
            Columns = 7,
            Cells = new List<EFCell>()
        };

        for (int row = 0; row < 6; row++)
        {
            for (int column = 0; column < 7; column++)
            {
                grid.Cells.Add(new EFCell
                {
                    Row = row,
                    Column = column,
                    Grid = grid // Associe chaque cellule à la grille
                });
            }
        }

        var player1 = new EFPlayer { Login = "Alice", PasswordHash = "hashedpassword1" };
        var player2 = new EFPlayer { Login = "Bob", PasswordHash = "hashedpassword2" };

        var game = new EFGame
        {
            Host = player1,
            Guest = player2,
            Grid = grid,
            Status = "In Progress"
        };

        // context.Players.AddRange(player1, player2);
        // context.Games.Add(game);
        // context.SaveChanges();
        
        await _playerRepository.AddAsync(player1);
        await _playerRepository.AddAsync(player2);
        await _gameRepository.AddAsync(game);
        
        Console.WriteLine("GameService: Test() - Game created");
    }

}