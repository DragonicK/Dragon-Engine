﻿using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpUpgradeData {
        public MessageHeader Header { get; set; } = MessageHeader.UpgradeData;        
        public int SelectedInventoryIndex { get; set; }        
        public int Break { get; set; }
        public int Success { get; set; }
        public int Reduce { get; set; }
        public int ReduceAmount { get; set; }
        public int Cost { get; set; }
        public DataRequirement[] Requirements { get; set; } = Array.Empty<DataRequirement>();
    }
}