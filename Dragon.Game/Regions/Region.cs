using Dragon.Game.Instances;

namespace Dragon.Game.Regions;

public sealed class Region {
    public int Id { get; set; }
    public int MapId { get; set; }
    public InstanceType Type { get; set; }
    public Link Link { get; set; }
    public List<IRegionEntity> Entities { get; set; }

    public Region() {
        Entities = new List<IRegionEntity>();
    }
}