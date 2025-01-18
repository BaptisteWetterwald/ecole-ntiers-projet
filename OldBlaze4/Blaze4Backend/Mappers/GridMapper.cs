using Blaze4.Application.Models;
using Blaze4Backend.DTOs;

namespace Blaze4Backend.Mappers;

public static class GridMapper
{
    public static GridDto ToDto(Grid grid)
    {
        var cells = new List<CellDto>();
        for (int row = 0; row < Grid.Rows; row++)
        {
            for (int col = 0; col < Grid.Columns; col++)
            {
                var cell = grid.Cells[row, col];
                cells.Add(CellMapper.ToDto(cell));
            }
        }

        return new GridDto
        {
            Rows = Grid.Rows,
            Columns = Grid.Columns,
            Cells = cells
        };
    }
}
