using Puissance4.Application.Domain;
using Puissance4.Application.DTOs;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class PlayerMapper
{
    public static Player ToDomain(EFPlayer entity)
    {
        return new Player
        {
            Id = entity.Id,
            Login = entity.Login
        };
    }

    public static EFPlayer ToEntity(Player domain)
    {
        return new EFPlayer
        {
            Id = domain.Id,
            Login = domain.Login
        };
    }
    
    public static PlayerDto ToDto(Player player)
    {
        return new PlayerDto
        {
            Id = player.Id,
            Login = player.Login
        };
    }
    
    public static PlayerDto ToDto(EFPlayer entity)
    {
        return new PlayerDto
        {
            Id = entity.Id,
            Login = entity.Login
        };
    }
}
