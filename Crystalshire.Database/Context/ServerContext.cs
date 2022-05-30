using Microsoft.EntityFrameworkCore;

namespace Crystalshire.Database.Context;

public class ServerContext : DbContext {

    public ServerContext(DbContextOptions<ServerContext> options) : base(options) {

    }
}