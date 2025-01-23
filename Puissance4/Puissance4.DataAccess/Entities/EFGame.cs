namespace Puissance4.DataAccess.Entities;

public class EFGame
{
    public int Id { get; set; } // Clé primaire

    // Relation avec Grid
    public EFGrid Grid { get; set; } // Chaque partie a une grille

    // Relation avec les Players
    public int HostId { get; set; }
    public EFPlayer Host { get; set; } // Joueur hôte

    public int? GuestId { get; set; }
    public EFPlayer? Guest { get; set; } // Joueur invité (peut être null)

    public int? WinnerId { get; set; }
    public EFPlayer? Winner { get; set; } // Gagnant (peut être null)

    public int? CurrentTurnId { get; set; }
    public EFPlayer? CurrentTurn { get; set; } // Joueur ayant le tour actuel (peut être null)

    public string Status { get; set; } // Statut du jeu (ex. "In Progress", "Completed")

    public static class Statuses
    {
        public const string AwaitingGuest = "Awaiting Guest";
        public const string InProgress = "In Progress";
        public const string Finished = "Finished";
    }
}