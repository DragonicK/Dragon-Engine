using Microsoft.EntityFrameworkCore;

namespace Dragon.Database.Context;

public class ServerContext : DbContext {

    public ServerContext(DbContextOptions<ServerContext> options) : base(options) {

    }
}