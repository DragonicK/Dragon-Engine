using Dragon.Core.Model;
using Dragon.Core.Services;

using System.Reflection;

namespace Dragon.Game.Administrator;

public sealed class CommandRepository : ICommandRepository {
    private readonly Dictionary<AdministratorCommands, IAdministratorCommand> commands;

    public IAdministratorCommand? GetType(AdministratorCommands command) {
        commands.TryGetValue(command, out var commandValue);

        return commandValue;
    }

    public CommandRepository(IServiceInjector injector) {
        commands = new Dictionary<AdministratorCommands, IAdministratorCommand>();

        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly
            .GetTypes()
            .Where(t => t.GetInterface(nameof(IAdministratorCommand)) is not null)
            .ToArray();

        foreach (var type in types) {
            var instance = Activator.CreateInstance(type, injector) as IAdministratorCommand;

            if (instance is not null) {
                var header = type.GetRuntimeProperty("Command")?.GetValue(instance);

                if (header is not null) {
                    commands.Add((AdministratorCommands)header, instance);
                }
            }
        }
    }
}