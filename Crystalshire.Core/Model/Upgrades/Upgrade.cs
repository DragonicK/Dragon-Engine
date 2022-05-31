namespace Crystalshire.Core.Model.Upgrades;

public class Upgrade {
    public int Id { get; set; }
    public int MaximumLevel => ContentLevel.Count;
    public IDictionary<int, UpgradeLevel> ContentLevel { get; set; }

    public Upgrade() {
        ContentLevel = new Dictionary<int, UpgradeLevel>();
    }
}