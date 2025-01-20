using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Puissance4.DataAccess.Entities;

public class TokenEntity
{
    [Key]
    public int Id { get; set; } // Clé primaire

    public string Color { get; set; } = string.Empty; // Propriété de couleur

    public int? CellId { get; set; } // Clé étrangère

    [ForeignKey(nameof(CellId))]
    public CellEntity? Cell { get; set; } // Relation avec CellEntity
}