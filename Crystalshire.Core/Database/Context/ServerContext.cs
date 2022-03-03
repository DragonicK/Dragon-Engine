using Microsoft.EntityFrameworkCore;

namespace Crystalshire.Core.Database.Context {
    public class ServerContext : DbContext {

        public ServerContext(DbContextOptions<ServerContext> options) : base(options) {

        }
    }
}