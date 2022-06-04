using Dragon.Core.Model;

using System.Reflection;

namespace Dragon.Game.Administrator;

public class CommandRepository : ICommandRepository {
    private readonly Dictionary<AdministratorCommands, Type> commands;

    public Type? GetType(AdministratorCommands command) {
        if (commands.ContainsKey(command)) {
            return commands[command];
        }

        return null;
    }

    public CommandRepository() {
        commands = new Dictionary<AdministratorCommands, Type>();

        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly
            .GetTypes()
            .Where(t => t.GetInterface("IAdministratorCommand") is not null)
            .ToArray();

        foreach (var type in types) {
            var instance = Activator.CreateInstance(type) as IAdministratorCommand;

            if (instance is not null) {
                var header = type.GetRuntimeProperty("Command")?.GetValue(instance);

                if (header is not null) {
                    commands.Add((AdministratorCommands)header, type);
                }
            }
        }
    }
}