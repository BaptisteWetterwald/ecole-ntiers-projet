using Puissance4.Application.Domain;
using Puissance4.Application.Mappers;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Services;

public class CellService
{
    private readonly ICellRepository _cellRepository;
    private readonly TokenService _tokenService;

    public CellService(ICellRepository cellRepository, TokenService tokenService)
    {
        _cellRepository = cellRepository;
        _tokenService = tokenService;
    }

    public Cell GetCellById(int id)
    {
        // Récupérer l'entité Cell depuis le repository
        var cellEntity = _cellRepository.GetById(id);
        if (cellEntity == null) throw new Exception("Cell not found.");

        // Récupérer le Token correspondant (si TokenId n'est pas nul)
        Token? token = cellEntity.TokenId.HasValue ? _tokenService.GetTokenById(cellEntity.TokenId.Value) : null;
        
        // Mapper l'entité en modèle métier
        return CellMapper.ToDomain(cellEntity, token);
    }
}