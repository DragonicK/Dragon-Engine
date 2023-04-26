using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Parties;
using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Instances;

namespace Dragon.Game.Services;

public sealed class InstanceService : IService, IUpdatableService {
    public ServicePriority Priority => ServicePriority.Last;
    public ContentService? ContentService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public PassphraseService? PassphraseService { get; private set; }
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
        LoadFields();

        var maximum = Configuration!.Trade.Maximum;
        Trades = new Dictionary<int, TradeManager>(maximum);
    }

    public void Stop() {
 
    }

    public void Update(int deltaTime) {
        UpdateTrades();
        UpdateParties();
        UpdateInstances();
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

    private void LoadFields() {
        var sender = PacketSenderService!.PacketSender;

        var loader = new InstanceLoader("./Server/Fields", Configuration!, ContentService!.Maps, sender);

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

    private void UpdateTrades() {
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
    }

    private void UpdateParties() {
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

    private void UpdateInstances() {
        foreach (var (_, instance) in Instances) {
            instance.Execute();  
        }
    }
}