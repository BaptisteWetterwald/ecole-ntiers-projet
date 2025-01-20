using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Puissance4.DataAccess.Entities;

public class CellEntity()
{
    [Key]
    public int Id { get; set; }
    public int GridId { get; set; }
    public int? TokenId { get; set; }
    
    [ForeignKey(nameof(TokenId))]
    public TokenEntity? Token { get; set; }
    
    public int Row { get; set; }
    public int Column { get; set; }
}