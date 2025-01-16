using Blaze4.Application.Models;
using Blaze4Backend.DTOs;

namespace Blaze4Backend.Mappers;

public static class CellMapper
{
    public static CellDto ToDto(Cell cell)
    {
        return new CellDto
        {
            Row = cell.Row,
            Column = cell.Column,
            Token = cell.Token != null ? TokenMapper.ToDto(cell.Token) : null
        };
    }
}
