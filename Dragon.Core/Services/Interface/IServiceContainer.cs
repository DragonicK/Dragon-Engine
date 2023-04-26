namespace Dragon.Core.Services;

public interface IServiceContainer {
    int Count { get; }
    void Add(Type type, IService instance);
    IService[] ToArray();
    IService this[Type type] { get; }
    IUpdatableService[] GetUpdatableServices();
}