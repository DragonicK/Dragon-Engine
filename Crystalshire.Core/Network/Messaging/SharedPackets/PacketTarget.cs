﻿using Crystalshire.Core.Model;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class PacketTarget : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.Target;
        public int Index { get; set; }
        public TargetType TargetType { get; set; }
    }
}