using Crystalshire.Core.Model;

namespace Crystalshire.Game.Administrator {
    public interface ICommandRepository {
        Type? GetType(AdministratorCommands command);
    }
}