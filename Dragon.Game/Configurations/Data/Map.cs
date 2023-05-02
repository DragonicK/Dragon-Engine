namespace Dragon.Game.Configurations.Data;

public sealed class Map {
    public int MaximumNpcs { get; set; }
    public int MaximumPlayers { get; set; }
    public int MaximumCorpses { get; set; }

    public Map() {
        MaximumNpcs = 255;
        MaximumPlayers = 255;
        MaximumCorpses = 255;
    }
}