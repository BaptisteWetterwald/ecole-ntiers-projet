using Blaze4.Application.Models;
using Blaze4Backend.DTOs;

namespace Blaze4Backend.Mappers;

public static class TokenMapper
{
    public static TokenDto ToDto(Token token)
    {
        return new TokenDto
        {
            Color = token.Color
        };
    }
}
