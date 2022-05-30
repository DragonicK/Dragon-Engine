namespace Crystalshire.Game.Configurations.Data;

public sealed class Guild {
    public bool Create { get; set; }
    public bool Delete { get; set; }
    public int MaximumMembers { get; set; }

    public Guild() {
        MaximumMembers = 100;
        Create = true;
        Delete = true;
    }
}