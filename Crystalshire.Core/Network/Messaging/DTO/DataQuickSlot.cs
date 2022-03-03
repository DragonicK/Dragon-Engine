using Crystalshire.Core.Model;

namespace Crystalshire.Core.Network.Messaging.DTO {
    public struct DataQuickSlot {
        
        public int Index { get; set; }

        
        public QuickSlotType ObjectType { get; set; }

        
        public int ObjectValue { get; set; }
    }
}