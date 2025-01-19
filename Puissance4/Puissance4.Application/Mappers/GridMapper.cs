using Puissance4.Application.Domain;
using Puissance4.DataAccess.Entities;

namespace Puissance4.Application.Mappers;

public static class GridMapper
{
    public static GridEntity ToEntity(Grid grid)
    {
        /*var cellIds = new int[grid.Rows, grid.Columns];
        for (var i = 0; i < grid.Rows; i++)
        {
            for (var j = 0; j < grid.Columns; j++)
            {
                cellIds[i, j] = grid.Cells[i, j].Id;
            }
        }*/
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

    public static Grid ToDomain(GridEntity gridEntity, Cell[,] cells)
    {
        var grid = new Grid(gridEntity.Rows, gridEntity.Columns)
        {
            Cells = cells
        };
        return grid;
    }
}