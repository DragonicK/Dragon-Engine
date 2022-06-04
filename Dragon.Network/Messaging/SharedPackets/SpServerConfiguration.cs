namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpServerConfiguration : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ServerConfiguration;
    public int MaximumAuras { get; set; }
    public int MaximumHeraldries { get; set; }
    public int MaximumInventories { get; set; }
    public int MaximumLevel { get; set; }
    public int MaximumRecipes { get; set; }
    public int MaximumSkills { get; set; }
    public int MaximumEffects { get; set; }
    public int MaximumTitles { get; set; }
    public int MaximumWarehouse { get; set; }
    public int MaximumMails { get; set; }
}