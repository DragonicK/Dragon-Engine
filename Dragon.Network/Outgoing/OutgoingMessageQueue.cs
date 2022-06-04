using Disruptor;
using Disruptor.Dsl;

namespace Dragon.Network.Outgoing;
public class OutgoingMessageQueue : IOutgoingMessageQueue {
    private const int BufferSize = 4096;

    private RingBuffer<RingBufferByteArray>? ringbuffer;
    private readonly Disruptor<RingBufferByteArray> disruptor;

    public IOutgoingMessageEventHandler OutgoingMessageEventHandler { get; }

    public OutgoingMessageQueue(IOutgoingMessageEventHandler outgoingMessageEventHandler) {
        OutgoingMessageEventHandler = outgoingMessageEventHandler;

        disruptor = new Disruptor<RingBufferByteArray>(() => new RingBufferByteArray(), BufferSize, TaskScheduler.Default, ProducerType.Multi, new SpinWaitWaitStrategy());

        disruptor.HandleEventsWith(OutgoingMessageEventHandler);
    }

    public void Start() {
        disruptor.Start();

        ringbuffer = disruptor.RingBuffer;
    }

    public void Stop() {
        disruptor.Halt();
    }

    public void Enqueue(RingBufferByteArray buffer) {
        var sequence = ringbuffer!.Next();
        var entry = ringbuffer[sequence];

        var bytes = new byte[buffer.Length];
        buffer.GetContent(ref bytes);

        entry.FromId = buffer.FromId;
        entry.DestinationPeers = buffer.DestinationPeers;
        entry.ExceptDestination = buffer.ExceptDestination;
        entry.TransmissionTarget = buffer.TransmissionTarget;

        entry.SetContent(bytes);

        ringbuffer.Publish(sequence);
    }
}