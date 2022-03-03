using Crystalshire.Core.Network.Messaging;

namespace Crystalshire.Core.Model.BlackMarket {
    public class BlackMarketItem {
        
        public int Id { get; set; }

        
        public int Price { get; set; }

        
        public BlackMarketItemCategory Category { get; set; }

        
        public int ItemId { get; set; }

        
        public int Level { get; set; }

        
        public bool Bound { get; set; }

        
        public int AttributeId { get; set; }

        
        public int UpgradeId { get; set; }

        
        public int MaximumStack { get; set; }

        
        public int PurchaseLimit { get; set; }

        
        public bool CouldSendGift { get; set; }
    }
}