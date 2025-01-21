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
        return new EFGrid
        {
            Rows = grid.Rows,
            Columns = grid.Columns,
            Cells = grid.Cells
                .Cast<Cell>() // Convertir le tableau 2D en IEnumerable<Cell>
                .Where(cell => cell.Token != null) // Ignorer les cellules sans Token
                .Select(CellMapper.ToEntity) // Mapper vers EFCell
                .ToList()
        };
    }
    
    public static GridDto ToDto(Grid grid)
    {
        var cellsDto = new List<List<CellDto>>();
        for (int row = 0; row < grid.Rows; row++)
        {
            var rowDto = new List<CellDto>();
            for (int column = 0; column < grid.Columns; column++)
            {
                rowDto.Add(CellMapper.ToDto(grid.Cells[row, column]));
            }
            cellsDto.Add(rowDto);
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
        var cellsDto = new List<List<CellDto>>();
        foreach (var efCell in efGrid.Cells)
        {
            var rowDto = new List<CellDto>();
            for (int column = 0; column < efGrid.Columns; column++)
            {
                rowDto.Add(CellMapper.ToDto(efCell));
            }
            cellsDto.Add(rowDto);
        }

        return new GridDto
        {
            Rows = efGrid.Rows,
            Columns = efGrid.Columns,
            Cells = cellsDto
        };
    }
}