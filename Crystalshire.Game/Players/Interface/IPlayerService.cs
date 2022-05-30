﻿using Crystalshire.Core.Model;
using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Accounts;
using Crystalshire.Core.Model.Premiums;

namespace Crystalshire.Game.Players;

public interface IPlayerService {
    IDatabase<Premium>? Premiums { get; set; }
    float Character { get; }
    float Party { get; }
    float Guild { get; }
    float Skill { get; }
    float Craft { get; }
    float Quest { get; }
    float Pet { get; }
    float GoldChance { get; }
    float GoldIncrease { get; }
    IDictionary<Rarity, float> ItemDrops { get; }

    IList<AccountService> ToArray();
    void Add(AccountService service);
    void AllocateRates();
    void CheckForExpiredServices();
}