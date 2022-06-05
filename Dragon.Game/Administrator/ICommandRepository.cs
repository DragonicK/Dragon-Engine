using Dragon.Core.Model;

namespace Dragon.Game.Administrator;

public interface ICommandRepository {
    Type? GetType(AdministratorCommands command);
}