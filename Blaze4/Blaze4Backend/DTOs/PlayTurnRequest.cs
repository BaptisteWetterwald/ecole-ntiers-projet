namespace Blaze4Backend.DTOs;

public class PlayTurnRequest
{
    public int GameId { get; set; }
    public int PlayerId { get; set; }
    public int Column { get; set; }
    
    public PlayTurnRequest(int gameId, int playerId, int column)
    {
        GameId = gameId;
        PlayerId = playerId;
        Column = column;
    }
}