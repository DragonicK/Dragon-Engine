using Dragon.Database;

using Dragon.Core.Logs;
using Dragon.Core.Services;

using Dragon.Login.Configurations;

namespace Dragon.Login.Services;

public class DatabaseService : IService {
    public ServicePriority Priority => ServicePriority.High;
    public IDatabaseFactory? DatabaseFactory { get; private set; }
    public ConfigurationService? Configuration { get; private set; }

    public void Start() {
        DatabaseFactory = new DatabaseFactory();

        CheckMembershipDatabase(Configuration);
        CheckServerpDatabase(Configuration);
    }

    public void Stop() {

    }

    private void CheckMembershipDatabase(IConfiguration? configuration) {
        OutputLog.Write("Checking Membership database");

        if (configuration is not null) {
            CheckMembershipConnection(DatabaseFactory, configuration);
        }
        else {
            OutputLog.Write("Configuration not found");
        }
    }

    private void CheckServerpDatabase(IConfiguration? configuration) {
        OutputLog.Write("Checking Server database");

        if (configuration is not null) {
            CheckServerConnection(DatabaseFactory, configuration);
        }
        else {
            OutputLog.Write("Configuration not found");
        }
    }

    private void CheckServerConnection(IDatabaseFactory? factory, IConfiguration configuration) {
        if (factory is not null) {
            var handler = factory.GetServerHandler(configuration.DatabaseServer);
            var result = handler.CanConnect();

            handler.Dispose();

            OutputLog.Write($"Server database is {(result ? "" : "not ")}connected");
        }
        else {
            OutputLog.Write($"Database factory not found");
        }
    }

    private void CheckMembershipConnection(IDatabaseFactory? factory, IConfiguration configuration) {
        if (factory is not null) {
            var handler = factory.GetMembershipHandler(configuration.DatabaseServer);
            var result = handler.CanConnect();

            handler.Dispose();

            OutputLog.Write($"Membership database is {(result ? "" : "not ")}connected");
        }
        else {
            OutputLog.Write($"Database factory not found");
        }
    }
}