using Puissance4.Application.Domain;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class TokenMapper
{
    public static Token ToDomain(TokenEntity entity)
    {
        return new Token(entity.Color){Id = entity.Id};
    }

    public static TokenEntity ToEntity(Token domain)
    {
        return new TokenEntity
        {
            Color = domain.Color
        };
    }
}