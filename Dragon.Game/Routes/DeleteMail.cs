using Dragon.Core.Services;
using Dragon.Core.Model.Mailing;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Players;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Routes;

public sealed class DeleteMail : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.DeleteMail;

    public DeleteMail(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var sender = GetPacketSender();
        var received = packet as PacketDeleteMail;

        if (received is not null) {
            var player = GetPlayerRepository().FindByConnectionId(connection.Id);

            if (player is not null) {
                Execute(sender, player, received);
            }
        }
    }

    private void Execute(IPacketSender sender, IPlayer player, PacketDeleteMail packet) {
        var list = new List<int>(packet.Id.Length);
        var isDeleted = true;

        foreach (var id in packet.Id) {
            var code = player.Mails.Delete(id);

            if (code == MailingOperationCode.Deleted) {
                list.Add(id);
            }
            else if (code == MailingOperationCode.AttachedNotReceived) {
                isDeleted = false;
            }
        }

        sender.SendDeleteMail(player, list.ToArray());

        if (!isDeleted) {
            sender.SendMailOperationResult(player, MailingOperationCode.AttachedNotReceived);
        }
    }
}