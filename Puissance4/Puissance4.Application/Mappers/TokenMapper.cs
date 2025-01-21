using Puissance4.Application.Domain;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class TokenMapper
{
    public static Token ToDomain(EFToken entity)
    {
        return new Token(entity.Color);
    }

    public static EFToken ToEntity(Token domain)
    {
        return new EFToken
        {
            Color = domain.Color
        };
    }
}