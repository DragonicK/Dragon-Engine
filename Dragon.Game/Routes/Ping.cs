﻿using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class Ping : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.Ping;

    public Ping(IServiceInjector injector) : base(injector) {
        injector.Inject(this);
    }

    public void Process(IConnection connection, object packet) {
        GetPacketSender().SendPing(connection);
    }
}