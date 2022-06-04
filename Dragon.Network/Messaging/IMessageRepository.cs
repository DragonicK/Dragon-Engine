namespace Dragon.Network.Messaging;

public interface IMessageRepository<T> {
    IDictionary<T, Type> Messages { get; }
    Type GetMessage(T header);
    bool Contains(T header);
}