﻿using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Configurations;

namespace Dragon.Game.Combat;

public class ExperienceHandler {
    public IConfiguration? Configuration { get; init; }
    public ContentService? ContentService { get; init; }

    public int GetExperience(IPlayer player, IInstanceEntity from) {
        var id = from.Id;
        var npcs = ContentService!.Npcs;

        if (npcs.Contains(id)) {
            var server = Configuration!.Rates.Character;
            var service = player.Services.Character;

            var experience = npcs[id]!.Experience;

            return Convert.ToInt32(experience * (server + service));
        }

        return 0;
    }

    public bool CheckForLevelUp(IPlayer player) {
        var levelCount = 0;

        var level = player.Character.Level;
        var experience = player.Character.Experience;
        var database = ContentService!.PlayerExperience;

        if (level >= database.MaximumLevel) {
            if (experience >= database.Experiences[level]) {
                return false;
            }
        }

        var points = player.Character.Points;
        var classId = player.Character.ClassCode;

        var classJob = ContentService!.Classes[classId]!;

        while (experience >= database.Experiences[level]) {
            experience -= database.Experiences[level]; ;

            points += classJob.AttributePointPerLevel;

            level++;
            levelCount++;

            if (level >= database.MaximumLevel) {
                break;
            }
        }

        if (level >= database.MaximumLevel) {
            if (experience > database.Experiences[level]) {
                experience = database.Experiences[level];
            }
        }

        player.Character.Points = points;
        player.Character.Level = level;
        player.Character.Experience = experience;

        return levelCount > 0;
    }
}