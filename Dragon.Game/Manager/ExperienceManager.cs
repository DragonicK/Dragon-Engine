using Dragon.Game.Services;
using Dragon.Game.Configurations;

namespace Dragon.Game.Manager;

public class ExperienceManager {
    public IConfiguration? Configuration { get; init; }
    public ContentService? ContentService { get; init; }

}