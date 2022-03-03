namespace Crystalshire.Core.Network.Messaging {
    public interface IMessageRepository<T> {
        Dictionary<T, Type> Messages { get; }
        Type GetMessage(T header);
        bool Contains(T header);
    }
}