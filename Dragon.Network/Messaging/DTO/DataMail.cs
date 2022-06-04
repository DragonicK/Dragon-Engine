namespace Dragon.Network.Messaging.DTO;

public sealed class DataMail {
    public int Id { get; set; }
    public string Sender { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool ReadFlag { get; set; }
    public int AttachedCurrency { get; set; }
    public bool AttachedCurrencyReceiveFlag { get; set; }
    public bool AttachItemReceiveFlag { get; set; }
    public string SendDate { get; set; } = string.Empty;
    public string ExpireDate { get; set; } = string.Empty;
    public int ItemId { get; set; }
    public int Value { get; set; }
    public int Level { get; set; }
    public bool Bound { get; set; }
    public int AttributeId { get; set; }
    public int UpgradeId { get; set; }
}