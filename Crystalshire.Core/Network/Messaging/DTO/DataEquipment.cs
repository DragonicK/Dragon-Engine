﻿using Crystalshire.Core.Model;

namespace Crystalshire.Core.Network.Messaging.DTO {
    public struct DataEquipment {
        
        public PlayerEquipmentType Index { get; set; }

        
        public int Id { get; set; }

        
        public int Level { get; set; }

        
        public bool Bound { get; set; }

        
        public int AttributeId { get; set; }

        
        public int UpgradeId { get; set; }
    }
}