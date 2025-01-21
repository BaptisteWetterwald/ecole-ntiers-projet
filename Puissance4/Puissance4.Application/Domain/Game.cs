namespace Puissance4.Application.Domain;

public class Game
{
    private static class Statuses
    {
        public const string AwaitingGuest = "Awaiting Guest";
        public const string InProgress = "In Progress";
        public const string Finished = "Finished";
    }
    
    public int Id { get; set; }
    public Grid Grid { get; set; }
    public Player Host { get; set; }
    public Player? Guest { get; set; }
    public Player? Winner { get; set; }
    public Player? CurrentTurn { get; set; }
    public string Status { get; set; } = Statuses.AwaitingGuest;
    
    public Game(Player host)
    {
        Host = host;
        CurrentTurn = null;
        Grid = new Grid
        {
            Rows = 6,
            Columns = 7,
            Cells = new Cell[6, 7]
        };
    }
    
    public void JoinGame(Player guest)
    {
        if (Guest != null) throw new InvalidOperationException("A guest has already joined.");
        if (guest == Host) throw new InvalidOperationException("Host cannot join their own game.");
        Guest = guest;
        CurrentTurn = Guest; // L'invité commence.
        Status = Statuses.InProgress;
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
}