using Puissance4.Application.Domain;
using Puissance4.Application.DTOs;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class GridMapper
{
    // Conversion de EFGrid à Grid (remplit les cellules vides)
    public static Grid ToDomain(EFGrid efGrid)
    {
        var grid = new Grid
        {
            Rows = efGrid.Rows,
            Columns = efGrid.Columns,
            Cells = new Cell[efGrid.Rows, efGrid.Columns]
        };
        
        for (int row = 0; row < efGrid.Rows; row++)
        {
            for (int column = 0; column < efGrid.Columns; column++)
            {
                grid.Cells[row, column] = new Cell(row, column);
            }
        }
        
        foreach (var efCell in efGrid.Cells)
        {
            grid.Cells[efCell.Row, efCell.Column] = CellMapper.ToDomain(efCell);
        }

        return grid;
    }

    // Conversion de Grid à EFGrid (ignorer les cellules sans Token)
    public static EFGrid ToEntity(Grid grid)
    {
        var efGrid = new EFGrid
        {
            Rows = grid.Rows,
            Columns = grid.Columns,
            Cells = new List<EFCell>()
        };
        
        for (int row = 0; row < grid.Rows; row++)
        {
            for (int column = 0; column < grid.Columns; column++)
            {
                var cell = grid.Cells[row, column];
                if (cell.Token == null) continue;
                efGrid.Cells.Add(CellMapper.ToEntity(cell));
            }
        }
        
        return efGrid;
    }
    
    public static GridDto ToDto(Grid grid)
    {
        var cellsDto = new List<CellDto>();
        for (int row = 0; row < grid.Rows; row++)
        {
            for (int column = 0; column < grid.Columns; column++)
            {
                cellsDto.Add(CellMapper.ToDto(grid.Cells[row, column]));
            }
        }
        
        return new GridDto
        {
            Rows = grid.Rows,
            Columns = grid.Columns,
            Cells = cellsDto
        };
    }
    
    public static GridDto ToDto(EFGrid efGrid)
    {
        var cells = new List<CellDto>();
        foreach (var efCell in efGrid.Cells)
        {
            cells.Add(CellMapper.ToDto(efCell));
        }
        
        return new GridDto
        {
            Rows = efGrid.Rows,
            Columns = efGrid.Columns,
            Cells = cells
        };
    }
}