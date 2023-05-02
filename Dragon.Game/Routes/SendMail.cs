using Dragon.Core.Services;
using Dragon.Core.Model.Characters;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Network;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class SendMail : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.SendMail;

    private readonly MailingManager MailingManager;

    public SendMail(IServiceInjector injector) : base(injector) {
        MailingManager = new MailingManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpSendMail;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }      
    }

    private void Execute(IPlayer player, CpSendMail packet) {
        if (IsValidPacket(packet)) {
            var receiver = packet.Receiver;
            var amount = packet.AttachAmount;
            var index = packet.AttachInventoryIndex;

            var mail = new CharacterMail() {
                Subject = packet.Subject,
                Content = packet.Content,
                AttachCurrency = packet.AttachCurrency
            };

            MailingManager.ProcessMailing(player, mail, receiver, index, amount);
        }
    }

    private bool IsValidPacket(CpSendMail packet) {
        if (string.IsNullOrEmpty(packet.Receiver) || PassedMaximumLength(packet.Receiver)) {
            return false;
        }

        if (string.IsNullOrEmpty(packet.Subject) || PassedMaximumLength(packet.Subject)) {
            return false;
        }

        if (string.IsNullOrEmpty(packet.Content) || PassedMaximumLength(packet.Content)) {
            return false;
        }

        if (packet.AttachCurrency < 0) {
            packet.AttachCurrency = 0;
        }

        if (packet.AttachAmount < 0) {
            packet.AttachAmount = 0;
        }

        return true;
    }

    private bool PassedMaximumLength(string t) {
        return t.Length > byte.MaxValue;
    }
}