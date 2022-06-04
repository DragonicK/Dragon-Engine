using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPlayerData : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PlayerData;
    public int Index { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccountLevel AccountLevel { get; set; }
    public int ClassCode { get; set; }
    public int Model { get; set; }
    public int Level { get; set; }
    public int MapId { get; set; }
    public short X { get; set; }
    public short Y { get; set; }
    public Direction Direction { get; set; }
    public int TitleId { get; set; }
    public bool IsDead { get; set; }
}