using System.Reflection;

namespace Dragon.Core.Services;

public sealed class ServiceBroker : IServiceBroker {

    private readonly Dictionary<Type, IService> services;

    public ServiceBroker() {
        services = new Dictionary<Type, IService>();
    }

    public IServiceContainer GetContainer() {
        var types = services.Select(pair => pair.Value).ToArray();

        return new ServiceContainer(types);
    }

    public void Start() {
        services.Clear();

        CreateServices();

        var types = services.Select(pair => pair.Value)
            .OrderBy(s => s.Priority)
            .ToArray();

        Inject(types);
        Start(types);
    }

    public void Stop() {
        var types = services.Select(pair => pair.Value)
            .OrderByDescending(s => s.Priority)
            .ToArray();

        Stop(types);
    }

    private void Start(IService[] instances) {
        foreach (var instance in instances) {
            instance.Start();
        }
    }

    private void Stop(IService[] instances) {
        foreach (var instance in instances) {
            instance.Stop();
        }
    }

    private void Inject(IService[] instances) {
        var container = new ServiceContainer(instances);
        var injector = new ServiceInjector(container);

        foreach (var instance in instances) {
            injector.Inject(instance);
        }
    }

    private void CreateServices() {
        var types = GetServiceTypes();

        if (types is not null) {
            foreach (var type in types) {
                var service = Activator.CreateInstance(type) as IService;

                if (service is not null) {
                    services.Add(service.GetType(), service);
                }
            }
        }
    }

    private Type[]? GetServiceTypes() {
        var assembly = Assembly.GetEntryAssembly();

        if (assembly is null) {
            return null;
        }

        return assembly
            .GetTypes()
            .Where(t => t.IsClass && t.GetInterface("IService") is not null)
            .ToArray();
    }
}