using System.Reflection;

namespace Dragon.Core.Services;

public class ServiceInjector : IServiceInjector {

    private readonly IServiceContainer _container;

    public ServiceInjector(IServiceContainer container) {
        _container = container;
    }

    public void Inject(object target) {
        InjectObject(target);
        // InjectArray(target);
        InjectContainer(target);
    }

    private void InjectObject(object target) {
        var targetType = target.GetType();
        var properties = targetType.GetRuntimeProperties()
            .Where(p => p.PropertyType.GetInterface("IService") is not null);

        var values = properties.Select(p => (p.Name, p.PropertyType)).ToArray();

        foreach (var (name, propertyType) in values) {
            var property = properties.Where(p => p.Name == name).First();

            property.SetValue(target, _container[propertyType]);
        }
    }

    private void InjectArray(object target) {
        var targetType = target.GetType();
        var properties = targetType.GetRuntimeProperties()
              .Where(p => p.PropertyType == typeof(IService[]))
              .FirstOrDefault();

        if (properties is not null) {
            properties.SetValue(target, _container.ToArray());
        }
    }

    private void InjectContainer(object target) {
        var targetType = target.GetType();
        var properties = targetType.GetRuntimeProperties()
              .Where(p => p.PropertyType == typeof(IServiceContainer))
              .FirstOrDefault();

        if (properties is not null) {
            properties.SetValue(target, _container);
        }
    }
}