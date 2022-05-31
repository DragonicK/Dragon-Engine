namespace Crystalshire.Core.Model.Premiums;

public class Premium {
    public int Id { get; set; }
    public string Name { get; set; }
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

    public Premium() {
        Name = string.Empty;

        ItemDrops = new Dictionary<Rarity, float>() {
                { Rarity.Common, 0f },
                { Rarity.Uncommon, 0f },
                { Rarity.Rare, 0f },
                { Rarity.Epic, 0f },
                { Rarity.Mythic, 0f },
                { Rarity.Ancient, 0f },
                { Rarity.Legendary, 0f },
                { Rarity.Ethereal, 0f },
            };
    }
}