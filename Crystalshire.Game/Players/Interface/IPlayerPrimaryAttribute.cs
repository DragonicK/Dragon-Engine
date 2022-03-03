using Crystalshire.Core.Model;

namespace Crystalshire.Game.Players {
    public interface IPlayerPrimaryAttribute {
        void Add(PrimaryAttribute attribute, int value);
        void Set(PrimaryAttribute attribute, int value);
        int Get(PrimaryAttribute attribute);
    }
}