using System.ComponentModel.DataAnnotations.Schema;

namespace Crystalshire.Core.Model.Characters {
    public class CharacterMail {
        public long Id { get; set; }
        public long SenderCharacterId { get; set; }
        public string SenderCharacterName { get; set; } = string.Empty;
        public long ReceiverCharacterId { get; set; }
        public byte MailTypeCode { get; set; }
        public byte DeliveryTypeCode { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime SendDate { get; set; }
        public bool ReadFlag { get; set; }
        public bool DeleteFlag { get; set; }
        public int AttachCurrency { get; set; }
        public bool AttachCurrencyReceiveFlag { get; set; }
        public bool AttachItemReceiveFlag { get; set; }
        public bool AttachItemFlag { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public byte MailTabCode { get; set; }

        [NotMapped]
        public CharacterMailAttachItem? MailAttachItem { get; set; }
        [NotMapped]
        public int Index { get; set; }
    }
}