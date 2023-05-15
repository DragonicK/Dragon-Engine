using Microsoft.EntityFrameworkCore;

namespace Dragon.Database.Context;

public sealed class GameContext : DbContext {

    public GameContext(DbContextOptions<GameContext> options) : base(options) {

    }
}