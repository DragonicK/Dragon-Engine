using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Services;

namespace Dragon.Game.Routes;

public sealed class AttributePoint {
    public IConnection? Connection { get; set; }
    public CpUseAttributePoint? Packet { get; set; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    public void Process() {
        var sender = PacketSenderService!.PacketSender;
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                var index = Packet!.Attribute;

                if (player.Character.Points > 0) {
                    player.PrimaryAttributes.Add(index, 1);

                    player.Character.Points--;

                    player.AllocateAttributes();

                    sender?.SendAttributes(player);
                }
            }
        }
    }
}