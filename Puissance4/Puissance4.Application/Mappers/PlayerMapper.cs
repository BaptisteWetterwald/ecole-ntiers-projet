using Puissance4.Application.Domain;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class PlayerMapper
{
    public static PlayerEntity ToEntity(Player player)
    {
        //var gamesIds = player.Games.Select(game => game.Id).ToList();

        var games = new List<GameEntity>();
        foreach (var game in player.Games)
        {
            games.Add(GameMapper.ToEntity(game));
        }
        
        return new PlayerEntity
        {
            Login = player.Login,
            PasswordHash = player.PasswordHash,
            Games = games
        };
    }
    
    public static Player ToDomain(PlayerEntity playerEntity, List<Game> games)
    {
        var player = new Player(playerEntity.Login, playerEntity.PasswordHash)
        {
            Games = games
        };
        return player;
    }
}