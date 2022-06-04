using Dragon.Core.Common;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpAuthentication : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Authentication;
    public string Username { get; set; } = string.Empty;
    public string Passphrase { get; set; } = string.Empty;
    public ClientVersion Version { get; set; }
    public string MachineId { get; set; } = string.Empty;
    public string BIOSId { get; set; } = string.Empty;
    public string HardDiskId { get; set; } = string.Empty;
    public string CPUId { get; set; } = string.Empty;
    public string VideoId { get; set; } = string.Empty;
    public string MacAddressId { get; set; } = string.Empty;
    public string MotherboardId { get; set; } = string.Empty;
}