using Dragon.Database;

using Dragon.Core.Logs;
using Dragon.Core.Services;

using Dragon.Game.Configurations;

namespace Dragon.Game.Services;

public sealed class DatabaseService : IService {
    public ServicePriority Priority => ServicePriority.High;
    public IDatabaseFactory? DatabaseFactory { get; private set; }
    public LoggerService? LoggerService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }

    public void Start() {
        DatabaseFactory = new DatabaseFactory();

        var logger = LoggerService?.Logger;

        CheckMembershipDatabase(logger, Configuration);
        CheckServerDatabase(logger, Configuration);
    }

    public void Stop() {

    }

    private void CheckMembershipDatabase(ILogger? logger, IConfiguration? configuration) {
        logger?.Info(GetType().Name, "Checking Membership database");

        if (configuration is not null) {
            CheckDatabaseConnection("Membership", logger, DatabaseFactory, configuration.DatabaseMembership);
        }
        else {
            logger?.Error(GetType().Name, "Membership configuration not found");
        }
    }

    private void CheckServerDatabase(ILogger? logger, IConfiguration? configuration) {
        logger?.Info(GetType().Name, "Checking Server database");

        if (configuration is not null) {
            CheckDatabaseConnection("Server", logger, DatabaseFactory, configuration.DatabaseServer);
        }
        else {
            logger?.Error(GetType().Name, "Server configuration not found");
        }
    }

    private void CheckDatabaseConnection(string name, ILogger? logger, IDatabaseFactory? factory, DBConfiguration configuration) {
        if (factory is not null) {
            var handler = factory.GetServerHandler(configuration);
            var result = handler.CanConnect();

            handler.Dispose();

            logger?.Info(GetType().Name, $"{name} database is {(result ? "" : "not ")}connected");
        }
        else {
            logger?.Error(GetType().Name, "Database factory not found");
        }
    }
}