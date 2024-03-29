﻿using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class ChangeModel : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.SetModel;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 2;

    public ChangeModel(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void Process(IPlayer administrator, string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                ContinueProcess(administrator, parameters);
            }
        }
    }

    private void ContinueProcess(IPlayer administrator, string[] parameters) {
        if (administrator.AccountLevel >= AccountLevel.Superior) {
            _ = int.TryParse(parameters[1], out var id);

            var repository = ConnectionService!.PlayerRepository;
            var target = repository!.FindByName(parameters[0].Trim());

            Set(administrator, target, id);
        }
    }

    private void Set(IPlayer administrator, IPlayer? player, int id) {
        var sender = GetPacketSender();

        if (player is not null) {
            if (id > 0) {
                player!.Character.Model = id;

                var instanceId = player!.Character.Model;
                var instances = InstanceService!.Instances;

                instances.TryGetValue(instanceId, out var instance);

                if (instance is not null) {
                    sender.SendPlayerModel(player, instance);
                }
            }
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, administrator);
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}