using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Characters;

using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Combat.Common;

namespace Dragon.Game.Combat.Handler;

public sealed class Aura : ISkillHandler {
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private int Id;
    private int Level;
    private int Range;
    private Effect? AttributeEffect;

    private readonly AuraManager AuraManager;
    private readonly List<Target> Targets;
    private readonly IPlayer Player;

    private const int SingleTarget = 1;

    public Aura(IServiceInjector injector, IPlayer player) {
        injector.Inject(this);

        Player = player;

        AuraManager = new AuraManager(injector);
      
        Targets = new List<Target>(SingleTarget) {
            new Target() {
                Entity = Player,
                Type = TargetType.Player
            }
        };
    }

    public bool CanSelect(ref Target target, SkillEffect effect) {
        return true;
    }

    public Damaged GetDamage(ref Target target, CharacterSkill inventory, SkillEffectType type) {
        Range = inventory.Range;
        Level = inventory.SkillLevel;

        return new Damaged();
    }

    public IList<Target> GetTarget(ref Target target, IInstance instance, CharacterSkill inventory, SkillEffect effect) {
        AttributeEffect = GetDatabaseEffect()[effect.EffectId];

        if (AttributeEffect is not null) {
            Id = AttributeEffect.Id;
        }

        return Targets;
    }

    public void Inflict(ref Damaged damaged, ref Target target, IInstance instance, SkillEffect effect) {
        if (Player.Auras.Contains(Id)) {
            Player.Auras.Remove(Id);

            AuraManager.DeactivateAura(Player, Id);
        }
        else {
            Player.Auras.Add(Id, Level, Range);

            AuraManager.ActivateAura(Player, Id, Level, Range);
        }
    }

    public void ResetTargets() {

    }

    private IDatabase<Effect> GetDatabaseEffect() {
        return ContentService!.Effects;
    }
}