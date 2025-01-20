using System.ComponentModel.DataAnnotations.Schema;

namespace Puissance4.DataAccess.Entities;

[NotMapped]
public class PlayerEntity
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public List<GameEntity> Games { get; set; } = new();
}