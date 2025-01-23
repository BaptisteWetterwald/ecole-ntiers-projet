using Puissance4.Application.Domain;
using Puissance4.DTOs;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class CellMapper
{
    public static Cell ToDomain(EFCell entity)
    {
        return new Cell(entity.Row, entity.Column)
        {
            Token = new Token(entity.TokenColor)
        };
    }

    public static EFCell ToEntity(Cell cell, int gridId)
    {
        if (cell.Token == null)
        {
            throw new InvalidOperationException("A cell cannot be saved without a token.");
        }

        return new EFCell
        {
            Row = cell.Row,
            Column = cell.Column,
            GridId = gridId,
            TokenColor = cell.Token.Color
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
            TokenColor = entity.TokenColor
        };
    }
}