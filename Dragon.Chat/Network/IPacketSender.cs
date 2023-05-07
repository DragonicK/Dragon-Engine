using Dragon.Core.Model;

using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Chat.Players;

namespace Dragon.Chat.Network;

public interface IPacketSender {
    void SendMessage(PacketBroadcastMessage message);
    void SendMessage(PacketBroadcastMessage message, IConnection connection);
    void SendMessage(PacketBroadcastMessage message, IList<IPlayer> players);
    void SendMessageBubble(SpMessageBubble bubble, IList<IPlayer> players);
    void SendMessage(SystemMessage message, QbColor color, IPlayer player, string[]? parameters = null);
}