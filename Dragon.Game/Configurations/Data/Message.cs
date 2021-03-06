namespace Dragon.Game.Configurations.Data;

public class Message {
    public int MaximumLength { get; set; }
    public int SayCooldown { get; set; }
    public int SayInterval { get; set; }
    public int SayCount { get; set; }
    public int WorldCooldown { get; set; }
    public int WorldInterval { get; set; }
    public int WorldCount { get; set; }
    public int PrivateCooldown { get; set; }
    public int PrivateInterval { get; set; }
    public int PrivateCount { get; set; }

    public Message() {
        MaximumLength = byte.MaxValue;
    }
}