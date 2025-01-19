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
    private readonly ITokenRepository _tokenRepository;

    public GameService(
        IGameRepository gameRepository,
        IPlayerRepository playerRepository,
        IGridRepository gridRepository,
        ICellRepository cellRepository,
        ITokenRepository tokenRepository
    )
    {
        _gameRepository = gameRepository;
        _playerRepository = playerRepository;
    }


    public object? CreateGame(int hostId)
    {
        Console.WriteLine("GameService: CreateGame(" + hostId + ")");
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
}