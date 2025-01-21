using Puissance4.Application.Domain;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class GridMapper
{
    public static GridEntity ToEntity(Grid grid)
    {
        // Créer une entité Grid
        var gridEntity = new GridEntity
        {
            Rows = grid.Rows,
            Columns = grid.Columns
        };

        // Mapper chaque Cell vers une CellEntity
        foreach (var cell in grid.Cells)
        {
            var cellEntity = CellMapper.ToEntity(cell);
            
            // Don't map empty cells
            if (cellEntity.Token == null)
            {
                continue;
            }
            
            gridEntity.Cells.Add(cellEntity);
        }

        Console.WriteLine($"GridMapper: ToEntity() - {gridEntity.Cells.Count} cells mapped");

        return gridEntity;
    }
    
    public static Grid ToDomain(GridEntity gridEntity)
    {
        // Initialiser un tableau 2D de cellules
        var cells = new Cell[gridEntity.Rows, gridEntity.Columns];

        // Mapper chaque CellEntity vers une Cell et remplir le tableau 2D
        foreach (var cellEntity in gridEntity.Cells)
        {
            var cell = CellMapper.ToDomain(cellEntity);
            cells[cellEntity.Row, cellEntity.Column] = cell;
        }

        return new Grid
        {
            Cells = cells
        };
    }
}