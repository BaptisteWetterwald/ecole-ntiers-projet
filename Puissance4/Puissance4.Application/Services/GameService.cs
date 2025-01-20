using Puissance4.Application.Domain;
using Puissance4.Application.Mappers;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Services;

public class GameService
{
    /*private readonly IGameRepository _gameRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IGridRepository _gridRepository;
    private readonly ICellRepository _cellRepository;*/
    private readonly ITokenRepository _tokenRepository;

    public GameService(
        /*IGameRepository gameRepository,
        IPlayerRepository playerRepository,
        IGridRepository gridRepository,
        ICellRepository cellRepository,*/
        ITokenRepository tokenRepository
    )
    {
        /*_gameRepository = gameRepository;
        _playerRepository = playerRepository;
        _gridRepository = gridRepository;
        _cellRepository = cellRepository;*/
        _tokenRepository = tokenRepository;
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

    public async Task<Token> Test()
    {
        Console.WriteLine("GameService: Test()");

        Token token = new Token("Red");
        TokenEntity tokenEntity = TokenMapper.ToEntity(token);
        await _tokenRepository.AddAsync(tokenEntity);
        
        await _tokenRepository.SaveChangesAsync();

        Console.WriteLine("Token ID is " + tokenEntity.Id);
        return token;
    }
}