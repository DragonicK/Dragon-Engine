namespace Crystalshire.Network.Incoming {
    public interface IIncomingMessageQueue {
        IIncomingMessageEventHandler IncomingMessageEventHandler { get; }
        void Start();
        void Stop();
        void Enqueue(int fromId, byte[] buffer);
    }
}