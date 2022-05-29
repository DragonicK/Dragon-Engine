using Crystalshire.Network;
using Crystalshire.Game.Players;
using Crystalshire.Game.Regions;
using Crystalshire.Game.Configurations;

namespace Crystalshire.Game.Instances {
    public interface IInstance {
        int Id { get; set; }
        int MapId { get; set; }
        int HighIndex { get; set; }
        int MaximumX { get; set; }
        int MaximumY { get; set; }
        Link Link { get; set; }
        IConfiguration Configuration { get; }
        IList<IRegionEntity> RegionEntities { get; set; }
        IList<IInstanceEntity> Entities { get; set; }
        IIndexGenerator IndexGenerator { get; set; }

        bool Add(IPlayer player);
        bool Contains(IPlayer player);
        bool Contains(int index);
        bool Remove(IPlayer player);
        IPlayer? Get(int index);
        IList<IPlayer> GetPlayers();
        bool IsBlocked(int x, int y);
    }
}