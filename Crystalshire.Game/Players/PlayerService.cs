using Crystalshire.Core.Model;
using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Accounts;
using Crystalshire.Core.Model.Premiums;

namespace Crystalshire.Game.Players;

public class PlayerService : IPlayerService {
    public IDatabase<Premium>? Premiums { get; set; }
    public float Character { get; private set; }
    public float Party { get; private set; }
    public float Guild { get; private set; }
    public float Skill { get; private set; }
    public float Craft { get; private set; }
    public float Quest { get; private set; }
    public float Pet { get; private set; }
    public float GoldChance { get; private set; }
    public float GoldIncrease { get; private set; }
    public IDictionary<Rarity, float> ItemDrops { get; }

    private readonly IList<AccountService> _services;
    private readonly long _accountId;

    public PlayerService(long accountId, IList<AccountService>? services) {
        _accountId = accountId;

        if (services is null) {
            _services = new List<AccountService>();
        }
        else {
            _services = services;
        }

        ItemDrops = new Dictionary<Rarity, float>();

        CheckForExpiredServices();
    }

    public void Add(AccountService service) {
        service.AccountId = _accountId;

        _services.Add(service);

        AllocateRates();
    }

    public void CheckForExpiredServices() {
        foreach (var service in _services) {
            if (!service.Expired) {
                service.Expired = IsDateExpired(service.EndTime);
            }
        }
    }

    public void AllocateRates() {
        Character = 0;
        Party = 0;
        Guild = 0;
        Skill = 0;
        Craft = 0;
        Quest = 0;
        Pet = 0;
        GoldChance = 0;
        GoldIncrease = 0;

        if (Premiums is not null) {
            foreach (var service in _services) {

                var premium = Premiums[service.ServiceId];

                if (premium is not null) {
                    Character += premium.Character;
                    Party += premium.Party;
                    Guild += premium.Guild;
                    Skill += premium.Skill;
                    Craft += premium.Craft;
                    Quest += premium.Quest;
                    Pet += premium.Pet;
                    GoldChance += premium.GoldChance;
                    GoldIncrease += premium.GoldIncrease;

                    foreach (var (index, value) in premium.ItemDrops) {
                        if (!ItemDrops.ContainsKey(index)) {
                            ItemDrops.Add(index, 0);
                        }

                        ItemDrops[index] += value;
                    }
                }
            }
        }
    }

    public IList<AccountService> ToArray() {
        return _services;
    }

    private static bool IsDateExpired(DateTime date) {
        return DateTime.Now.CompareTo(date) == 1;
    }

}