using Crystalshire.Core.Model;
using Crystalshire.Core.Services;
using Crystalshire.Core.Serialization;
using Crystalshire.Core.Model.Maps;

using Crystalshire.Game.Parties;
using Crystalshire.Game.Manager;
using Crystalshire.Game.Players;
using Crystalshire.Game.Instances;

namespace Crystalshire.Game.Services {
    public class InstanceService : IService, IUpdatableService {
        public ServicePriority Priority => ServicePriority.Mid;
        public MapPassphrase? Passphrases { get; private set; }
        public ContentService? ContentService { get; private set; }
        public ConfigurationService? Configuration { get; private set; }
        public PacketSenderService? PacketSenderService { get; private set; }
        public IDictionary<int, IInstance> Instances { get; set; }
        public IDictionary<int, TradeManager> Trades { get; set; }
        public IDictionary<int, PartyManager> Parties { get; set; }

        public InstanceService() {
            Instances = new Dictionary<int, IInstance>();
            Trades = new Dictionary<int, TradeManager>();
            Parties = new Dictionary<int, PartyManager>();
        }

        public void Start() {
            LoadPassphrases();
            LoadFields();

            var maximum = Configuration!.Trade.Maximum;
            Trades = new Dictionary<int, TradeManager>(maximum);
        }

        public void Stop() {
            Passphrases?.Clear();
        }

        public void Update(int deltaTime) {
            var tradeTimeOut = Configuration!.Trade.AcceptTimeOut;

            foreach (var (id, trade) in Trades) {
                if (trade.CanConclude()) {
                    trade.ExecuteTrade();
                }

                if (trade.State == TradeState.Waiting) {
                    trade.AcceptTimeOut++;

                    if (trade.AcceptTimeOut >= tradeTimeOut) {
                        trade.UpdateFailed();
                    }
                }
                else if (trade.State == TradeState.Failed) {
                    Trades.Remove(id);
                }
                else if (trade.State == TradeState.Concluded) {
                    Trades.Remove(id);
                }
            }

            foreach (var (id, party) in Parties) {
                if (party.State == PartyState.Waiting) {

                    if (party.IsCreationFailed()) {
                        party.UpdateFailedCreation();
                    }
                }
                else if (party.State == PartyState.Created) {
                    party.CheckForPendingInvitations();
                    party.CheckForDisconnectedMembers();
                }
                else if (party.State == PartyState.Disbanded) {
                    Parties.Remove(id);
                }
            }
        }

        public int RegisterTrade(IPlayer starter, IPlayer invited) {
            var maximum = Configuration!.Trade.Maximum;
            var maximumTradeItems = Configuration!.Trade.MaximumTradeItems;

            for (var i = 1; i <= maximum; ++i) {
                if (!Trades.ContainsKey(i)) {

                    Trades.Add(i, new TradeManager(i, maximumTradeItems, starter, invited) {
                        PacketSender = PacketSenderService!.PacketSender,
                        Items = ContentService!.Items
                    });

                    return i;
                }
            }

            return 0;
        }

        public int RegisterParty() {
            var maximum = Configuration!.Party.Maximum;
            var inviteTimeOut = Configuration!.Party.InviteTimeOut;
            var maximumMembers = Configuration!.Party.MaximumMembers;
            var disconnectionTimeOut = Configuration!.Party.DisconnectionTimeOut;

            for (var i = 1; i <= maximum; ++i) {
                if (!Parties.ContainsKey(i)) {

                    Parties.Add(i, new PartyManager(i, inviteTimeOut, maximumMembers, disconnectionTimeOut) {
                        PacketSender = PacketSenderService!.PacketSender,
                    });

                    return i;
                }
            }

            return 0;
        }

        private void LoadPassphrases() {
            const string File = "./Server/Passphrases.json";

            if (!Json.FileExists(File)) {
                var instance = new MapPassphrase();
                instance.Add(1, "be10ea8c4e5d464cc0e75621ceb360e4");
                instance.Add(2, "be10ea8c4e5d464cc0e75621ceb360e4");
                instance.Add(3, "be10ea8c4e5d464cc0e75621ceb360e4");

                Json.Save(File, instance);
            }
            else {
                Passphrases = Json.Get<MapPassphrase>(File);

                if (Passphrases is not null) {
                    Json.Save(File, Passphrases);
                }
            }
        }    

        private void LoadFields() {
            var loader = new InstanceLoader("./Server/Fields", Configuration!, ContentService!.Maps);

            var manager = new InstanceEntityManager() {
                Npcs = ContentService!.Npcs,
                NpcAttributes = ContentService!.NpcAttributes
            };

            loader.LoadInstances();

            Instances = loader.Instances;

            foreach (var (_, instance) in Instances) {
                manager.CreateEntities(instance);
            }
        }
    }
}