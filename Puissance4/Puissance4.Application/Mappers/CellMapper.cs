using Puissance4.Application.Domain;
using Puissance4.Application.Services;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Mappers;

public static class CellMapper
{
    // use TokenService to get the token from the token repository
    private static TokenService _tokenService;
    
    public static CellEntity ToEntity(Cell cell)
    {
        return new CellEntity
        {
            Row = cell.Row,
            Column = cell.Column,
            TokenId = cell.Token?.Id
        };
    }

    public static Cell ToDomain(CellEntity cellEntity, Token? token)
    {
        return new Cell(cellEntity.Row, cellEntity.Column)
        {
            Token = token
        };
    }
}