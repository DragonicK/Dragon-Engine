using Microsoft.EntityFrameworkCore;

namespace Crystalshire.Core.Database.Context {
    public class GameContext : DbContext {

        public GameContext(DbContextOptions<GameContext> options) : base(options) {

        }
    }
}