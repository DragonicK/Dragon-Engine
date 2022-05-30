using Crystalshire.Core.Model;

namespace Crystalshire.Game.Instances;

public struct InstanceEntityPosition {
    public TargetType TargetType { get; set; }
    public int Index { get; set; }

    public void SetEntity(TargetType targetType, int index) {
        TargetType = targetType;
        Index = index;
    }

    public void Clear() {
        TargetType = TargetType.None;
        Index = 0;
    }
}