using Crystalshire.Core.Model;

namespace Crystalshire.Game.Configurations.Data;

public class Rate {
    public float Character { get; set; }
    public float Party { get; set; }
    public float Guild { get; set; }
    public float Skill { get; set; }
    public float Craft { get; set; }
    public float Quest { get; set; }
    public float Pet { get; set; }
    public float GoldChance { get; set; }
    public float GoldIncrease { get; set; }
    public IDictionary<Rarity, float> ItemDrops { get; set; }

    public Rate() {
        Pet = 1;
        Party = 1;
        Guild = 1;
        Skill = 1;
        Craft = 1;
        Quest = 1;
        Character = 1;

        GoldChance = 1;
        GoldIncrease = 1;

        ItemDrops = new Dictionary<Rarity, float>() {
                { Rarity.Common, 1.0f },
                { Rarity.Uncommon, 1.0f },
                { Rarity.Rare, 1.0f },
                { Rarity.Epic, 1.0f },
                { Rarity.Mythic, 1.0f },
                { Rarity.Ancient, 1.0f },
                { Rarity.Legendary, 1.0f },
                { Rarity.Ethereal, 1.0f },
            };
    }
}