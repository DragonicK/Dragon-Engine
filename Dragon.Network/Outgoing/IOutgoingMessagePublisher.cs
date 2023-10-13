using Dragon.Network.Pool;

namespace Dragon.Network.Outgoing;

public interface IOutgoingMessagePublisher {
    IConnectionRepository ConnectionRepository { get; }

    void Broadcast(TransmissionTarget transmissionPeer, IList<int> destination, int exceptDestination, IEngineBufferWriter buffer);
}