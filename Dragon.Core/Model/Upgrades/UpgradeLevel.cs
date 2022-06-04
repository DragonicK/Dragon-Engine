namespace Dragon.Core.Model.Upgrades;

public class UpgradeLevel {
    public IList<UpgradeRequirement> Requirements { get; set; }
    public int Success { get; set; }
    public int Break { get; set; }
    public int Reduce { get; set; }
    public int Cost { get; set; }

    public UpgradeLevel() {
        Requirements = new List<UpgradeRequirement>();
    }
}