using Dragon.Network.Pool;

namespace Dragon.Network;

public sealed class RingBufferByteArray {
    private const int ListSize = 64;

    public long Sequence { get; set; }
    public int FromId { get; set; }
    public IConnection? Connection { get; set; }
    public TransmissionTarget TransmissionTarget { get; set; }
    public List<int> DestinationPeers { get; set; }
    public int ExceptDestination { get; set; }
    public IEngineBufferReader? BufferReader { get; private set; }
    public IEngineBufferWriter? BufferWriter { get; private set; }

    public RingBufferByteArray() {
        DestinationPeers = new List<int>(ListSize);
    }

    public void SetOutgoingContent(IEngineBufferWriter buffer) => BufferWriter = buffer;

    public void SetIncomingContent(IEngineBufferReader buffer) => BufferReader = buffer;

    public void Reset() {
        Connection = null;
        BufferReader = null;
        BufferWriter = null;

        DestinationPeers.Clear();
    }
}