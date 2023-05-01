using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpActionMessage : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ActionMessage;
    public ActionMessageType MessageType { get; set; }
    public ActionMessageFontType FontType { get; set; }
    public QbColor Color { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Message { get; set; }
}