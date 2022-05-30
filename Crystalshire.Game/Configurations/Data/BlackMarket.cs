namespace Crystalshire.Game.Configurations.Data;

public class BlackMarket {
    public bool Enabled { get; set; }
    public int MaximumItemPerPage { get; set; }
    public string Sender { get; set; }
    public string PurchaseTitle { get; set; }
    public string PurchaseMessage { get; set; }
    public string GiftTitle { get; set; }
    public string GiftMessage { get; set; }

    public BlackMarket() {
        Enabled = true;
        MaximumItemPerPage = 6;
        Sender = "Black Market";
        PurchaseTitle = "「買い物」 Item Purchase";
        PurchaseMessage = "「買い物」 Item Purchase";
        GiftTitle = "「メッセージ」 New Message";
        GiftMessage = "「プレゼント」 You received a gift from {NAME}.";
    }
}