using Microsoft.EntityFrameworkCore;

namespace Dragon.Database.Context;

public class GameContext : DbContext {

    public GameContext(DbContextOptions<GameContext> options) : base(options) {

    }
}