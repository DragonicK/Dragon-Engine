using Crystalshire.Network;
using Crystalshire.Network.Outgoing;
using Crystalshire.Network.Messaging;

using Crystalshire.Core.Services;

namespace Crystalshire.Login.Services;

public class OutgoingMessageService : IService {
    public ServicePriority Priority => ServicePriority.Mid;
    public IOutgoingMessageQueue? OutgoingMessageQueue { get; private set; }
    public IOutgoingMessageEventHandler? OutgoingMessageEventHandler { get; private set; }
    public IOutgoingMessagePublisher? OutgoingMessagePublisher { get; private set; }
    public IOutgoingMessageWriter? OutgoingMessageWriter { get; private set; }
    public ISerializer? Serializer { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }

    public void Start() {
        var repository = ConnectionService!.ConnectionRepository!;

        Serializer = new MessageSerializer();

        OutgoingMessagePublisher = new OutgoingMessagePublisher(repository);
        OutgoingMessageEventHandler = new OutgoingMessageEventHandler(OutgoingMessagePublisher);
        OutgoingMessageQueue = new OutgoingMessageQueue(OutgoingMessageEventHandler);
        OutgoingMessageWriter = new OutgoingMessageWriter(OutgoingMessageQueue, Serializer);

        OutgoingMessageQueue.Start();
    }

    public void Stop() {
        OutgoingMessageQueue?.Stop();
    }
}