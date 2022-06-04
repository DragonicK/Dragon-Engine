using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Model.Mailing;

using Dragon.Game.Services;

namespace Dragon.Game.Routes;

public sealed class DeleteMail {
    public IConnection? Connection { get; set; }
    public PacketDeleteMail? Packet { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public PacketSenderService? PacketSenderService { get; set; }

    public void Process() {
        var sender = PacketSenderService!.PacketSender;
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {

                var isDeleted = true;

                var list = new List<int>(Packet!.Id.Length);

                foreach (var id in Packet!.Id) {
                    var code = player.Mails.Delete(id);

                    if (code == MailingOperationCode.Deleted) {
                        list.Add(id);
                    }
                    else if (code == MailingOperationCode.AttachedNotReceived) {
                        isDeleted = false;
                    }
                }

                sender!.SendDeleteMail(player, list.ToArray());

                if (!isDeleted) {
                    sender!.SendMailOperationResult(player, MailingOperationCode.AttachedNotReceived);
                }
            }
        }
    }
}