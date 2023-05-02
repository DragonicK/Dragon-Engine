using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class AttributePoint : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.UseAttributePoint;

    public AttributePoint(IServiceInjector injector) : base(injector) { }
 
    public void Process(IConnection connection, object packet) {
        var received = packet as CpUseAttributePoint;

        if (received is not null) {
            Execute(connection, received);
        }
    }

    private void Execute(IConnection connection, CpUseAttributePoint packet) {
        var sender = GetPacketSender();
        var player = FindByConnection(connection);

        if (player is not null) {
            var index = packet.Attribute;

            if (player.Character.Points > 0) {
                player.PrimaryAttributes.Add(index, 1);

                player.Character.Points--;
                player.AllocateAttributes();

                sender.SendAttributes(player);
                sender.SendPlayerVital(player);
                sender.SendPartyVital(player);
            }
        }
    }
}