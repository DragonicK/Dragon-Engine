namespace Dragon.Core.Services;

public sealed class ServiceContainer : IServiceContainer {
    private readonly Dictionary<Type, IService> container;
    public int Count => container.Count;

    public ServiceContainer() => container = new Dictionary<Type, IService>();

    public ServiceContainer(IService[] instances) {
        container = new Dictionary<Type, IService>();

        foreach (var instance in instances) {
            container.Add(instance.GetType(), instance);
        }
    }

    public void Add(Type type, IService instance) => container.Add(type, instance);

    public IService[] ToArray() => container.Select(pair => pair.Value).ToArray();

    public IService this[Type type] => container[type];

    public IUpdatableService[] GetUpdatableServices() {
        var list = new List<IUpdatableService>();

        foreach (var (_, value) in container) {
            if (value is IUpdatableService service) {
                list.Add(service);
            }
        }

        return list.ToArray();
    }
}