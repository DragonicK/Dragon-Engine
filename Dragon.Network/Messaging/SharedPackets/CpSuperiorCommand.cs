using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpSuperiorCommand : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SuperiorCommand;
    public AdministratorCommands Command { get; set; }
    public string[]? Parameters { get; set; } = Array.Empty<string>();
}