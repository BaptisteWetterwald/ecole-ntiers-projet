using Puissance4.Application.Domain;
using Puissance4.Application.DTOs;
using Puissance4.Application.Mappers;
using Puissance4.DataAccess.Entities;

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

        // Initialisation des cellules vides
        for (int row = 0; row < grid.Rows; row++)
        {
            for (int column = 0; column < grid.Columns; column++)
            {
                grid.Cells[row, column] = new Cell(row, column)
                {
                    Token = null // Cellule vide par défaut
                };
            }
        }

        // Remplissage des cellules avec un Token
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
        var cellsDto = new CellDto[grid.Rows, grid.Columns];
        for (int row = 0; row < grid.Rows; row++)
        {
            for (int column = 0; column < grid.Columns; column++)
            {
                cellsDto[row, column] = CellMapper.ToDto(grid.Cells[row, column]);
            }
        }

        return new GridDto
        {
            Rows = grid.Rows,
            Columns = grid.Columns,
            Cells = cellsDto
        };
    }
}