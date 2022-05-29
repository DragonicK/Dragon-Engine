using Crystalshire.Network;
using Crystalshire.Network.Messaging.DTO;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Core.Model.Mailing;
using Crystalshire.Core.Model.Characters;

using Crystalshire.Game.Players;

namespace Crystalshire.Game.Network {
    public sealed partial class PacketSender {

        public void SendMailOperationResult(IPlayer player, MailingOperationCode code) {
            var packet = new SpMailOperationResult() {
                OperationCode = code
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }

        public void SendMails(IPlayer player) {
            var mails = player.Mails.ToList();

            var packet = new SpMailing() {
                Mails = new DataMail[mails.Count]
            };

            for (var i = 0; i < mails.Count; ++i) {
                var mail = mails[i];

                packet.Mails[i] = new DataMail() {
                    Id = mail.Index,
                    Sender = mail.SenderCharacterName,
                    Subject = mail.Subject,
                    Content = mail.Content,
                    ReadFlag = mail.ReadFlag,
                    AttachedCurrency = mail.AttachCurrency,
                    AttachedCurrencyReceiveFlag = mail.AttachCurrencyReceiveFlag,
                    AttachItemReceiveFlag = mail.AttachItemReceiveFlag,
                    SendDate = $"{mail.SendDate.ToShortDateString()} {mail.SendDate.ToShortTimeString()}",
                    ExpireDate = $"{mail.ExpireDate.ToShortDateString()} {mail.ExpireDate.ToShortTimeString()}",
                };

                var item = mail.MailAttachItem;

                if (item is not null) {
                    packet.Mails[i].ItemId = item.ItemId;
                    packet.Mails[i].Value = item.Value;
                    packet.Mails[i].Level = item.Level;
                    packet.Mails[i].AttributeId = item.AttributeId;
                    packet.Mails[i].UpgradeId = item.UpgradeId;
                }
            }

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }

        public void SendDeleteMail(IPlayer player, int[] id) {
            var packet = new PacketDeleteMail() {
                Id = id
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }

        public void SendMailUpdate(IPlayer player, int id, bool currencyReceiveFlag, bool itemReceiveFlag) {
            var packet = new SpUpdateMail() {
                Id = id,
                AttachItemReceiveFlag = itemReceiveFlag,
                AttachCurrencyReceiveFlag = currencyReceiveFlag
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }

        public void SendMail(IPlayer player, CharacterMail mail) {
            var packet = new SpAddMail() {
                Mail = new DataMail() {
                    Id = mail.Index,
                    Sender = mail.SenderCharacterName,
                    Subject = mail.Subject,
                    Content = mail.Content,
                    ReadFlag = mail.ReadFlag,
                    AttachedCurrency = mail.AttachCurrency,
                    AttachedCurrencyReceiveFlag = mail.AttachCurrencyReceiveFlag,
                    AttachItemReceiveFlag = mail.AttachItemReceiveFlag,
                    SendDate = $"{mail.SendDate.ToShortDateString()} {mail.SendDate.ToShortTimeString()}",
                    ExpireDate = $"{mail.ExpireDate.ToShortDateString()} {mail.ExpireDate.ToShortTimeString()}",
                }
            };

            var item = mail.MailAttachItem;

            if (item is not null) {
                packet.Mail.ItemId = item.ItemId;
                packet.Mail.Value = item.Value;
                packet.Mail.Level = item.Level;
                packet.Mail.AttributeId = item.AttributeId;
                packet.Mail.UpgradeId = item.UpgradeId;
            }

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }
    }
}