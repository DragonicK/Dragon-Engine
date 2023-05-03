using Dragon.Network;

using Dragon.Chat.Messages;

namespace Dragon.Chat.Network;

public interface IPacketSender {
    void SendMessage(Message message, IConnection connection);
    void SendMessageBubble(Bubble bubble, IConnection connection);
}