namespace Puissance4.Application.Domain;

public class Player
{
    public int Id { get; set; }
    public string Login { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var player = (Player) obj;
        return Id == player.Id;
    }
}
