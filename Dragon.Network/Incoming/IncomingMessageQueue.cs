﻿using Disruptor;
using Disruptor.Dsl;

namespace Dragon.Network.Incoming;
public class IncomingMessageQueue : IIncomingMessageQueue {
    private const int BufferSize = 4096;

    private RingBuffer<RingBufferByteArray>? ringbuffer;
    private readonly Disruptor<RingBufferByteArray> disruptor;

    public IIncomingMessageEventHandler IncomingMessageEventHandler { get; }

    public IncomingMessageQueue(IIncomingMessageEventHandler incomingMessageEventHandler) {
        IncomingMessageEventHandler = incomingMessageEventHandler;

        disruptor = new Disruptor<RingBufferByteArray>(() => new RingBufferByteArray(), BufferSize, TaskScheduler.Default, ProducerType.Single, new BlockingWaitStrategy());

        disruptor.HandleEventsWith(IncomingMessageEventHandler);
    }

    public void Start() {
        disruptor.Start();

        ringbuffer = disruptor.RingBuffer;
    }

    public void Stop() {
        disruptor.Halt();
    }

    public void Enqueue(IConnection connection, int fromId, byte[] buffer) {
        if (ringbuffer is not null) {
            var sequence = ringbuffer.Next();
            var entry = ringbuffer[sequence];

            entry.FromId = fromId;
            entry.Connection = connection;

            entry.SetContent(buffer, buffer.Length);

            ringbuffer.Publish(sequence);
        }
    }
}