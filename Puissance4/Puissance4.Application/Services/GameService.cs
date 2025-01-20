using Puissance4.Application.Domain;
using Puissance4.Application.Mappers;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Services;

public class GameService
{
    /*private readonly IGameRepository _gameRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IGridRepository _gridRepository;*/
    private readonly ICellRepository _cellRepository;

    public GameService(
        /*IGameRepository gameRepository,
        IPlayerRepository playerRepository,
        IGridRepository gridRepository,*/
        ICellRepository cellRepository
    )
    {
        /*_gameRepository = gameRepository;
        _playerRepository = playerRepository;
        _gridRepository = gridRepository;*/
        _cellRepository = cellRepository;
    }


    public async Task<Game> CreateGame(int hostId)
    {
        /*
        Console.WriteLine("GameService: CreateGame(" + hostId + ")");
        PlayerEntity? hostEntity = await _playerRepository.GetByLoginAsync("baptiste");
        if (hostEntity == null) throw new Exception("Player not found");
        
        Player host = PlayerMapper.ToDomainWithoutGames(hostEntity);
        Grid grid = new Grid(6, 7);
        GridEntity gridEntity = GridMapper.ToEntity(grid);
        
        await _gridRepository.AddAsync(gridEntity);
        
        Game game = new Game(host)
        {
            Grid = grid
        };

        await _gameRepository.AddAsync(GameMapper.ToEntity(game));
        await _gameRepository.SaveChangesAsync();
        return game;
        */
        throw new NotImplementedException();
    }

    public object? JoinGame(int id, int guestId)
    {
        Console.WriteLine("GameService: JoinGame(" + id + ", " + guestId + ")");
        throw new NotImplementedException();
    }

    public object? PlayTurn(int gameId, int playerId, int column)
    {
        Console.WriteLine("GameService: PlayTurn(" + gameId + ", " + playerId + ", " + column + ")");
        throw new NotImplementedException();
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

}