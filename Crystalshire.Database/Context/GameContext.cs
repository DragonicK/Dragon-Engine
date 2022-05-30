using Microsoft.EntityFrameworkCore;

namespace Crystalshire.Database.Context;

public class GameContext : DbContext {

    public GameContext(DbContextOptions<GameContext> options) : base(options) {

    }
}