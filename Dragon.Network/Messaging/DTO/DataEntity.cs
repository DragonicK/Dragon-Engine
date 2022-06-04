using Dragon.Core.Model;

namespace Dragon.Network.Messaging.DTO;

public struct DataEntity {
    public int Index { get; set; }
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsDead { get; set; }
    public Direction Direction { get; set; }
    public int[] Vital { get; set; } = Array.Empty<int>();
    public int[] MaximumVital { get; set; } = Array.Empty<int>();

    public DataEntity() {
        Index = 0;
        Id = 0;
        X = 0;
        Y = 0;
        IsDead = false;
        Direction = Direction.Up;
    }
}