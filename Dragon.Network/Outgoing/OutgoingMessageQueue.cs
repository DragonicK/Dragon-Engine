using Disruptor;
using Disruptor.Dsl;

namespace Dragon.Network.Outgoing;
public class OutgoingMessageQueue : IOutgoingMessageQueue {
    private const int BufferSize = 4096;

    private RingBuffer<RingBufferByteArray>? ringbuffer;
    private Disruptor<RingBufferByteArray> disruptor;

    public IOutgoingMessageEventHandler OutgoingMessageEventHandler { get; }

    public OutgoingMessageQueue(IOutgoingMessageEventHandler outgoingMessageEventHandler) {
        OutgoingMessageEventHandler = outgoingMessageEventHandler;

        disruptor = new Disruptor<RingBufferByteArray>(() => new RingBufferByteArray(), BufferSize, TaskScheduler.Default, ProducerType.Multi, new BlockingWaitStrategy());

        disruptor.HandleEventsWith(OutgoingMessageEventHandler);
    }

    public void Start() {
        disruptor.Start();

        ringbuffer = disruptor.RingBuffer;
    }

    public void Stop() {
        disruptor.Halt();
    }

    public RingBufferByteArray GetNextSequence() {
        var sequence = ringbuffer!.Next();
        var buffer = ringbuffer[sequence];

        buffer.Sequence = sequence;

        return buffer;
    }

    public void Enqueue(RingBufferByteArray ringBuffer) {
        ringbuffer.Publish(ringBuffer.Sequence);
    }
}