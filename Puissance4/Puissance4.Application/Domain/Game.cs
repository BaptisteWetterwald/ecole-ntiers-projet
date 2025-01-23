namespace Puissance4.Application.Domain;

public class Game
{
    public Game(Player host)
    {
        Host = host;
        CurrentTurn = null;
        Grid = new Grid();
    }

    public int Id { get; set; }
    public Grid Grid { get; set; }
    public Player Host { get; set; }
    public Player? Guest { get; set; }
    public Player? Winner { get; set; }
    public Player? CurrentTurn { get; set; }
    public string Status { get; set; } = Statuses.AwaitingGuest;

    public void JoinGame(Player guest)
    {
        if (Guest != null) throw new InvalidOperationException("A guest has already joined.");
        if (guest.Equals(Host)) throw new InvalidOperationException("Host cannot join their own game.");
        Guest = guest;
        CurrentTurn = Guest; // L'invité commence.
        Status = Statuses.InProgress;
    }

    public void PlayTurn(Player player, int column)
    {
        if (!player.Equals(CurrentTurn)) throw new InvalidOperationException("Not your turn.");
        if (Status != Statuses.InProgress) throw new InvalidOperationException("Game is not in progress.");
        if (column < 0 || column >= Grid.Columns) throw new InvalidOperationException("Invalid column.");

        var token = new Token(player.Equals(Host) ? "Red" : "Yellow");
        Grid.DropToken(column, token);

        if (Grid.CheckWin())
        {
            Status = Statuses.Finished;
            Winner = player;
        }
        else if (Grid.IsFull())
        {
            Status = Statuses.Finished; // Match nul
        }
        else
        {
            SwitchTurn();
        }

        Console.WriteLine("Grid updated: " + Grid);
    }

    private void SwitchTurn()
    {
        CurrentTurn = CurrentTurn!.Equals(Host) ? Guest : Host;
    }

    public static class Statuses
    {
        public const string AwaitingGuest = "Awaiting Guest";
        public const string InProgress = "In Progress";
        public const string Finished = "Finished";
    }
}