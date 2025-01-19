/*
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
*/
using Puissance4.Application.Domain;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class PlayerMapper
{
    public static PlayerEntity ToEntity(Player player)
    {
        return new PlayerEntity
        {
            Login = player.Login,
            PasswordHash = player.PasswordHash,
            // On ne mappe pas les jeux eux-mêmes, seulement les IDs
            Games = new List<GameEntity>() // Les jeux doivent être gérés ailleurs
        };
    }

    public static Player ToDomain(PlayerEntity playerEntity, Func<int, List<Game>> getGamesForPlayer)
    {
        // Récupérer les jeux du joueur via la fonction `getGamesForPlayer`
        var games = getGamesForPlayer(playerEntity.Id);

        return new Player(playerEntity.Login, playerEntity.PasswordHash)
        {
            Games = games
        };
    }

    public static Player ToDomainWithoutGames(PlayerEntity playerEntity)
    {
        // Mapper le joueur sans inclure ses jeux
        return new Player(playerEntity.Login, playerEntity.PasswordHash);
    }
}
