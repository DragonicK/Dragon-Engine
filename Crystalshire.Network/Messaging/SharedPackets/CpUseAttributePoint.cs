﻿using Crystalshire.Core.Model;

namespace Crystalshire.Network.Messaging.SharedPackets;

public class CpUseAttributePoint : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.UseAttributePoint;
    public PrimaryAttribute Attribute { get; set; }
}