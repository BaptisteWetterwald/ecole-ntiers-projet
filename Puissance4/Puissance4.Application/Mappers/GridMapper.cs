using Puissance4.Application.Domain;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class GridMapper
{
    public static GridEntity ToEntity(Grid grid)
    {
        var cells = new List<CellEntity>();
        foreach (var cell in grid.Cells)
        {
            cells.Add(CellMapper.ToEntity(cell));
        }
        return new GridEntity
        {
            Rows = grid.Rows,
            Columns = grid.Columns,
            //CellsId = cellIds
            Cells = cells
        };
    }
    
    public static Grid ToDomain(GridEntity gridEntity)
    {
        // Initialiser un tableau 2D de cellules
        var cells = new Cell[gridEntity.Rows, gridEntity.Columns];

        // Mapper chaque CellEntity vers une Cell et remplir le tableau 2D
        foreach (var cellEntity in gridEntity.Cells)
        {
            var cell = CellMapper.ToDomain(cellEntity, null);
            cells[cellEntity.Row, cellEntity.Column] = cell;
        }

        return new Grid(gridEntity.Rows, gridEntity.Columns)
        {
            Cells = cells
        };
    }
}