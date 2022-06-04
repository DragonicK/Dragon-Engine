using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Model.Characters;

using Dragon.Game.Manager;
using Dragon.Game.Services;

namespace Dragon.Game.Routes;

public sealed class SendMail {
    public IConnection? Connection { get; set; }
    public CpSendMail? Packet { get; set; }
    public LoggerService? LoggerService { get; init; }
    public ContentService? ContentService { get; init; }
    public ConfigurationService? Configuration { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }
    public DatabaseService? DatabaseService { get; init; }

    public void Process() {
        if (IsValidPacket()) {
            var sender = PacketSenderService!.PacketSender;
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {

                    var receiver = Packet!.Receiver;
                    var index = Packet!.AttachInventoryIndex;
                    var amount = Packet!.AttachAmount;

                    var mail = new CharacterMail() {
                        Subject = Packet!.Subject,
                        Content = Packet!.Content,
                        AttachCurrency = Packet!.AttachCurrency
                    };

                    var manager = new MailingManager() {
                        Player = player,
                        PacketSender = sender,
                        PlayerRepository = repository,
                        Configuration = Configuration,
                        Factory = DatabaseService!.DatabaseFactory
                    };

                    manager.ProcessMailing(mail, receiver, index, amount);

                }
            }
        }
    }

    private bool IsValidPacket() {
        if (string.IsNullOrEmpty(Packet!.Receiver) || PassedMaximumLength(Packet!.Receiver)) {
            return false;
        }

        if (string.IsNullOrEmpty(Packet!.Subject) || PassedMaximumLength(Packet!.Subject)) {
            return false;
        }

        if (string.IsNullOrEmpty(Packet!.Content) || PassedMaximumLength(Packet!.Content)) {
            return false;
        }

        if (Packet!.AttachCurrency < 0) {
            Packet!.AttachCurrency = 0;
        }

        if (Packet!.AttachAmount < 0) {
            Packet!.AttachAmount = 0;
        }

        return true;
    }

    private bool PassedMaximumLength(string t) {
        return t.Length > byte.MaxValue;
    }
}