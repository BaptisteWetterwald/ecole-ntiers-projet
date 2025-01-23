using Puissance4.Application.Domain;
using Puissance4.DTOs;
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
        
        foreach (var cell in grid.Cells)
        {
            if (cell.Token != null) // Sauvegarder uniquement les cellules avec un token
            {
                efGrid.Cells.Add(CellMapper.ToEntity(cell, efGrid.Id));
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
        var cellsDto = efGrid.Cells.Select(CellMapper.ToDto).ToList();
        return new GridDto
        {
            Rows = efGrid.Rows,
            Columns = efGrid.Columns,
            Cells = cellsDto
        };
    }
}