namespace Dragon.Game.Configurations.Data;

public sealed class ProhibitedNames {
    public List<string> Names { get; set; }

    public ProhibitedNames() {
        Names = new List<string>() {
                "Admin",
                "GameMaster",
                "Administrator",
                "Administrador"
            };
    }

    public bool IsProhibited(string name) {
        return Names.Contains(name);
    }
}