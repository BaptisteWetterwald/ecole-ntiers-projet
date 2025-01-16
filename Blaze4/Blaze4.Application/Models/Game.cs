namespace Blaze4.Application.Models;

public class Game
{
    
    public static class Statuses
    {
        public const string AwaitingGuest = "Awaiting Guest";
        public const string InProgress = "In Progress";
        public const string Finished = "Finished";
    }
    
    public Player Host { get; private set; }
    public Player? Guest { get; private set; }
    public Grid Grid { get; private set; } = new();
    public string Status { get; private set; } = Statuses.AwaitingGuest;
    public Player? Winner { get; private set; }
    public Player? CurrentTurn { get; private set; }

    public Game(Player host)
    {
        Host = host;
        CurrentTurn = null;
    }

    public void JoinGame(Player guest)
    {
        if (Guest != null) throw new InvalidOperationException("A guest has already joined.");
        Guest = guest;
        CurrentTurn = Guest; // L'invité commence.
    }

    public void PlayTurn(Player player, int column)
    {
        if (player != CurrentTurn) throw new InvalidOperationException("Not your turn.");
        if (Status != "In Progress") throw new InvalidOperationException("Game is not in progress.");

        var token = new Token(player == Host ? "Red" : "Yellow");
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
    }

    private void SwitchTurn()
    {
        CurrentTurn = CurrentTurn == Host ? Guest : Host;
    }

    public void StartGame()
    {
        if (Guest == null) throw new InvalidOperationException("A guest must join before starting.");
        Status = Statuses.InProgress;
    }
}

