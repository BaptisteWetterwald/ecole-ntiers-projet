using Puissance4.Application.Domain;
using Puissance4.Application.Mappers;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Services;

public class TokenService
{
    private readonly ITokenRepository _tokenRepository;
    
    public TokenService(ITokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }

    public void AddToken(Token token)
    {
        // Mapper le modèle métier en entité
        var tokenEntity = TokenMapper.ToEntity(token);
        _tokenRepository.Add(tokenEntity);
        _tokenRepository.SaveChanges();
    }

    public Token GetTokenById(int id)
    {
        // Récupérer l'entité depuis le repository
        var tokenEntity = _tokenRepository.GetById(id);
        if (tokenEntity == null) throw new Exception("Token not found.");

        // Mapper l'entité en modèle métier
        return TokenMapper.ToDomain(tokenEntity);
    }
    
}