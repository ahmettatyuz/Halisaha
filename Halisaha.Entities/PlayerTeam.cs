namespace Halisaha.Entities;

public class PlayerTeam
{
    public int PlayerId { get; set; }
    public Player Player { get; set; }

    public int TeamId { get; set; }
    public Team Team { get; set; }
}
