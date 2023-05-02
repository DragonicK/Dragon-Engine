using Dragon.Core.Model;

namespace Dragon.Game.Administrator;

public interface ICommandRepository {
    IAdministratorCommand? GetType(AdministratorCommands command);
}