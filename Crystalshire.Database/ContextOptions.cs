using Microsoft.EntityFrameworkCore;
using Crystalshire.Database.Context;

namespace Crystalshire.Database {
    public static class ContextOptions {
        public static DbContextOptions<MembershipContext> GetMembershipOptions(DBConfiguration configuration) {
            var builder = new DbContextOptionsBuilder<MembershipContext>();
            builder.UseSqlServer(configuration.GetConnectionString());

            return builder.Options;
        }

        public static DbContextOptions<ServerContext> GetServerOptions(DBConfiguration configuration) {
            var builder = new DbContextOptionsBuilder<ServerContext>();
            builder.UseSqlServer(configuration.GetConnectionString());

            return builder.Options;
        }

        public static DbContextOptions<GameContext> GetGameOptions(DBConfiguration configuration) {
            var builder = new DbContextOptionsBuilder<GameContext>();
            builder.UseSqlServer(configuration.GetConnectionString());

            return builder.Options;
        }
    }
}