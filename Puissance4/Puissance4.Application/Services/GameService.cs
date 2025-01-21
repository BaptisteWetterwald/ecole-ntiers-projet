using Puissance4.Application.Domain;
using Puissance4.Application.Mappers;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Services;

public class GameService
{
    /*private readonly IGameRepository _gameRepository;
    private readonly IPlayerRepository _playerRepository;*/
    private readonly IGridRepository _gridRepository;
    private readonly ICellRepository _cellRepository;

    public GameService(
        /*IGameRepository gameRepository,
        IPlayerRepository playerRepository,*/
        IGridRepository gridRepository,
        ICellRepository cellRepository
    )
    {
        /*_gameRepository = gameRepository;
        _playerRepository = playerRepository;*/
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

    public async Task<Cell> Test()
    {
        Console.WriteLine("GameService: Test()");

        var token = new Token("Red");
        var cell = new Cell(1, 1)
        {
            Token = token
        };

        // Mapper les entités
        var cellEntity = CellMapper.ToEntity(cell);

        // Ajouter la cellule et son token via _cellRepository
        await _cellRepository.AddCellWithTokenAsync(cellEntity, cellEntity.Token);

        // Mapper le résultat final en objet métier
        var savedCell = CellMapper.ToDomain(cellEntity);

        Console.WriteLine($"Saved Cell: ID={savedCell.Id}, Token={savedCell.Token?.Color}");
        return savedCell;
    }
    
    // same but with the token directly in the cell
    public async Task<Cell> Test2()
    {
        Console.WriteLine("GameService: Test2()");

        var token = new Token("Red");
        var cell = new Cell(2, 2)
        {
            Token = token
        };

        // Mapper les entités
        var cellEntity = CellMapper.ToEntity(cell);

        // Ajouter la cellule et son token via _cellRepository
        await _cellRepository.AddCellWithTokenAsync(cellEntity);

        // Mapper le résultat final en objet métier
        var savedCell = CellMapper.ToDomain(cellEntity);

        Console.WriteLine($"Saved Cell: ID={savedCell.Id}, Token={savedCell.Token?.Color}");
        return savedCell;
    }
    
    // test the deletion of a cell
    public async Task Test3()
    {
        Console.WriteLine("GameService: Test3()");
        
        // Get a cell
        var cellEntity = await _cellRepository.GetCellAt(1, 1, 1);
        if (cellEntity == null) throw new Exception("Cell not found");
        
        // Delete the cell
        _cellRepository.Delete(cellEntity);
    }

    public async Task<Grid> Test4()
    {
        Console.WriteLine("GameService: Test4()");
        
        // Create a grid
        var grid = new Grid();
        
        // Add a few tokens to the grid
        grid.DropToken(0, new Token("Red"));
        grid.DropToken(1, new Token("Yellow"));
        grid.DropToken(2, new Token("Red"));
        
        // Save the grid
        var gridEntity = GridMapper.ToEntity(grid);
        await _gridRepository.AddGridWithCellsAsync(gridEntity);
        
        return grid;
    }
}