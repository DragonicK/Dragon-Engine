namespace Crystalshire.Core.Network.Incoming {
    public interface IIncomingMessageParser {
        IConnectionRepository? ConnectionRepository { get; init; }
        IPacketRouter? PacketRouter { get; init; }
        public void Process(int id, dynamic packet);
    }
}