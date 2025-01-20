using Puissance4.Application.Domain;
using Puissance4.Application.Services;
using Puissance4.DataAccess.Entities;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.Application.Mappers;

public static class CellMapper
{
    
    public static CellEntity ToEntity(Cell cell)
    {
        return new CellEntity
        {
            Row = cell.Row,
            Column = cell.Column,
            TokenId = cell.Token?.Id,
            Token = cell.Token != null ? TokenMapper.ToEntity(cell.Token) : null
        };
    }

    public static Cell ToDomain(CellEntity cellEntity)
    {
        return new Cell(cellEntity.Row, cellEntity.Column)
        {
            Id = cellEntity.Id, // Récupère l'ID de la cellule
            Token = cellEntity.Token != null ? TokenMapper.ToDomain(cellEntity.Token) : null // Mappe le Token si présent
        };
    }
}