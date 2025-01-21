using Puissance4.Application.Domain;
using Puissance4.Application.DTOs;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class CellMapper
{
    public static Cell ToDomain(EFCell entity)
    {
        return new Cell(entity.Row, entity.Column)
        {
            Token = entity.Token != null ? TokenMapper.ToDomain(entity.Token) : null
        };
    }

    public static EFCell ToEntity(Cell domain)
    {
        return new EFCell
        {
            Row = domain.Row,
            Column = domain.Column,
            Token = domain.Token != null ? TokenMapper.ToEntity(domain.Token) : null
        };
    }
    
    public static CellDto ToDto(Cell cell)
    {
        return new CellDto
        {
            Row = cell.Row,
            Column = cell.Column,
            TokenColor = cell.Token?.Color
        };
    }
    
    public static CellDto ToDto(EFCell entity)
    {
        return new CellDto
        {
            Row = entity.Row,
            Column = entity.Column,
            TokenColor = entity.Token?.Color
        };
    }
}