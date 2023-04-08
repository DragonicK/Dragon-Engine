using Dragon.Network;
using Dragon.Network.Outgoing;
using Dragon.Network.Messaging.DTO;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Model;
using Dragon.Core.Model.Shops;
using Dragon.Core.Model.Premiums;
using Dragon.Core.Model.Upgrades;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Configurations;
using Dragon.Game.Instances.Chests;

namespace Dragon.Game.Network;

public sealed partial class PacketSender : IPacketSender {
    public IOutgoingMessageWriter? Writer { get; set; }
    public IConfiguration? Configuration { get; set; }
    public PassphraseService? PassphraseService { get; set; }
    public InstanceService? InstanceService { get; set; }


    public void SendGettingMap(IPlayer player, bool isGettingMap) {
        var packet = new SpGettingMap() {
            IsGettingMap = isGettingMap
        };

        var msg = Writer.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendCharacters(IPlayer player) {
        var maximum = Configuration.Character.Maximum;

        var packet = new SpCharacters() {
            Characters = new DataCharacter[maximum]
        };

        for (var i = 0; i < player.Characters.Count; i++) {
            if (i < maximum) {
                var character = player.Characters[i];

                var _character = new DataCharacter() {
                    Name = character.Name,
                    Model = character.Model,
                    Class = character.ClassCode
                };

                if (character.DeleteRequest is not null) {
                    var now = DateTime.Now.Subtract((DateTime)character.DeleteRequest.ExclusionDate);

                    _character.Hour = now.Hours * -1;
                    _character.Minute = now.Minutes * -1;
                    _character.Second = now.Seconds * -1;

                    _character.PendingExclusion = character.DeleteRequest.IsActive;
                }

                packet.Characters[i] = _character;
            }
        }

        var msg = Writer.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendPlayerIndex(IPlayer player) {
        var packet = new SpPlayerIndex() {
            Index = player.IndexOnInstance
        };

        var msg = Writer.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendLoadMap(IPlayer player) {
        var passphrases = PassphraseService!.Passphrases;

        var id = player.Character.Map;
        var key = passphrases!.GetKey(id);
        var iv = passphrases!.GetIv(id);

        var packet = new SpLoadMap() {
            MapId = id,
            Key = key,
            Iv = iv
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendPlayerData(IPlayer player) {
        var packet = CreatePlayerDataPacket(player);

        var msg = Writer.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendPlayerDataTo(IPlayer player, IInstance instance) {
        var players = instance.GetPlayers();

        var packet = CreatePlayerDataPacket(player);

        var msg = Writer.CreateMessage(packet);

        msg.DestinationPeers.AddRange(players.Select(p => p.GetConnection().Id));
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendHighIndex(IInstance instance) {
        var players = instance.GetPlayers();

        var packet = new SpHighIndex() {
            Index = instance.HighIndex
        };

        var msg = Writer.CreateMessage(packet);

        msg.DestinationPeers.AddRange(players.Select(p => p.GetConnection().Id));
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendClearPlayers(IPlayer player) {
        var msg = Writer.CreateMessage(new SpClearPlayers());

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendPlayerXY(IPlayer player) {
        var packet = new SpPlayerXY() {
            Index = player.IndexOnInstance,
            X = player.Character.X,
            Y = player.Character.Y,
            Direction = player.Character.Direction
        };

        var msg = Writer.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendPlayerXY(IPlayer player, IInstance instance) {
        var players = instance.GetPlayers();

        var packet = new SpPlayerXY() {
            Index = player.IndexOnInstance,
            X = player.Character.X,
            Y = player.Character.Y,
            Direction = player.Character.Direction
        };

        var msg = Writer.CreateMessage(packet);

        msg.DestinationPeers.AddRange(players.Select(p => p.GetConnection().Id));
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendPlayersOnMapTo(IPlayer player, IInstance instance) {
        var players = instance.GetPlayers();
        var connectionId = player.GetConnection().Id;

        foreach (var p in players) {
            var packet = CreatePlayerDataPacket(p);

            var msg = Writer.CreateMessage(packet);

            msg.DestinationPeers.Add(connectionId);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }
    }

    public void SendPlayerVital(IPlayer player) {
        var health = CreatePlayerHpPacket(player);
        var mana = CreatePlayerMpPacket(player);

        var _health = Writer.CreateMessage(health);
        var _mana = Writer.CreateMessage(mana);

        var connectionId = player.GetConnection().Id;

        _health.DestinationPeers.Add(connectionId);
        _health.TransmissionTarget = TransmissionTarget.Destination;

        _mana.DestinationPeers.Add(connectionId);
        _mana.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(_health);
        Writer.Enqueue(_mana);

        if (player.PartyId > 0) {
            SendPartyVital(player);
        }
    }

    public void SendPlayerVital(IPlayer player, IInstance instance) {
        var players = instance.GetPlayers();

        var health = CreatePlayerHpPacket(player);
        var mana = CreatePlayerMpPacket(player);

        var _health = Writer!.CreateMessage(health);
        var _mana = Writer!.CreateMessage(mana);

        var list = players.Select(p => p.GetConnection().Id);

        _health.DestinationPeers.AddRange(list);
        _health.TransmissionTarget = TransmissionTarget.Destination;

        _mana.DestinationPeers.AddRange(list);
        _mana.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(_health);
        Writer.Enqueue(_mana);

        if (player.PartyId > 0) {
            SendPartyVital(player);
        }
    }

    public void SendAttributes(IPlayer player) {
        var attributes = player.GetAttributes();

        var packet = new SpPlayerAttributes() {
            Points = player.Character.Points,
            Strength = attributes.Get(PrimaryAttribute.Strength),
            Agility = attributes.Get(PrimaryAttribute.Agility),
            Constitution = attributes.Get(PrimaryAttribute.Constitution),
            Intelligence = attributes.Get(PrimaryAttribute.Intelligence),
            Spirit = attributes.Get(PrimaryAttribute.Spirit),
            Will = attributes.Get(PrimaryAttribute.Will),
            Attack = attributes.Get(SecondaryAttribute.Attack),
            Defense = attributes.Get(SecondaryAttribute.Defense),
            Accuracy = attributes.Get(SecondaryAttribute.Accuracy),
            Evasion = attributes.Get(SecondaryAttribute.Evasion),
            Parry = attributes.Get(SecondaryAttribute.Parry),
            Block = attributes.Get(SecondaryAttribute.Block),
            MagicAttack = attributes.Get(SecondaryAttribute.MagicAttack),
            MagicDefense = attributes.Get(SecondaryAttribute.MagicDefense),
            MagicAccuracy = attributes.Get(SecondaryAttribute.MagicAccuracy),
            MagicResist = attributes.Get(SecondaryAttribute.MagicResist),
            Concentration = attributes.Get(SecondaryAttribute.Concentration),
            CritRate = Convert.ToInt32(attributes.Get(UniqueAttribute.CritRate) * 100),
            CritDamage = Convert.ToInt32(attributes.Get(UniqueAttribute.CritDamage) * 100),
            ResistCritRate = Convert.ToInt32(attributes.Get(UniqueAttribute.ResistCritRate) * 100),
            ResistCritDamage = Convert.ToInt32(attributes.Get(UniqueAttribute.ResistCritDamage) * 100),
            DamageSuppression = Convert.ToInt32(attributes.Get(UniqueAttribute.DamageSuppression) * 100),
            HealingPower = Convert.ToInt32(attributes.Get(UniqueAttribute.HealingPower) * 100),
            FinalDamage = Convert.ToInt32(attributes.Get(UniqueAttribute.FinalDamage) * 100),
            Amplification = Convert.ToInt32(attributes.Get(UniqueAttribute.Amplification) * 100),
            Enmity = Convert.ToInt32(attributes.Get(UniqueAttribute.Enmity) * 100),
            AttackSpeed = Convert.ToInt32(attributes.Get(UniqueAttribute.AttackSpeed) * 100),
            CastSpeed = Convert.ToInt32(attributes.Get(UniqueAttribute.CastSpeed) * 100),

            ResistSilence = attributes.Get(SecondaryAttribute.SilenceResistance),
            ResistBlind = attributes.Get(SecondaryAttribute.BlindResistance),
            ResistStun = attributes.Get(SecondaryAttribute.StunResistance),
            ResistStumble = attributes.Get(SecondaryAttribute.StumbleResistance),

            PveAttack = Convert.ToInt32(attributes.Get(UniqueAttribute.PveAttack) * 100),
            PveDefense = Convert.ToInt32(attributes.Get(UniqueAttribute.PveDefense) * 100),
            PvpAttack = Convert.ToInt32(attributes.Get(UniqueAttribute.PvpAttack) * 100),
            PvpDefense = Convert.ToInt32(attributes.Get(UniqueAttribute.PvpDefense) * 100),

            FireAttack = attributes.GetElementAttack(ElementAttribute.Fire),
            WaterAttack = attributes.GetElementAttack(ElementAttribute.Water),
            EarthAttack = attributes.GetElementAttack(ElementAttribute.Earth),
            WindAttack = attributes.GetElementAttack(ElementAttribute.Wind),
            LightAttack = attributes.GetElementAttack(ElementAttribute.Light),
            DarkAttack = attributes.GetElementAttack(ElementAttribute.Dark),

            FireDefense = attributes.GetElementDefense(ElementAttribute.Fire),
            WaterDefense = attributes.GetElementDefense(ElementAttribute.Water),
            EarthDefense = attributes.GetElementDefense(ElementAttribute.Earth),
            WindDefense = attributes.GetElementDefense(ElementAttribute.Wind),
            LightDefense = attributes.GetElementDefense(ElementAttribute.Light),
            DarkDefense = attributes.GetElementDefense(ElementAttribute.Dark),
        };

        var msg = Writer.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendDirection(IPlayer player, IInstance instance) {
        var players = instance.GetPlayers();
        var list = players.Select(p => p.GetConnection().Id);

        var packet = new PacketPlayerDirection() {
            Direction = player.Character.Direction,
            Index = player.IndexOnInstance
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.ExceptDestination = player.GetConnection().Id;
        msg.TransmissionTarget = TransmissionTarget.BroadcastExcept;

        Writer.Enqueue(msg);
    }

    public void SendPlayerMovement(IPlayer player, MovementType movement, IInstance instance) {
        var players = instance.GetPlayers();
        var list = players.Select(p => p.GetConnection().Id);

        var packet = new PacketPlayerMovement() {
            Index = player.IndexOnInstance,
            Direction = player.Character.Direction,
            State = movement,
            X = (short)player.Character.X,
            Y = (short)player.Character.Y
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.ExceptDestination = player.GetConnection().Id;
        msg.TransmissionTarget = TransmissionTarget.BroadcastExcept;

        Writer.Enqueue(msg);
    }

    public void SendPlayerLeft(IPlayer player, IInstance instance, int index) {
        var players = instance.GetPlayers();
        var list = players.Select(p => p.GetConnection().Id);

        var packet = new SpPlayerLeft() {
            Index = index,
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.ExceptDestination = player.GetConnection().Id;
        msg.TransmissionTarget = TransmissionTarget.BroadcastExcept;

        Writer.Enqueue(msg);
    }

    public void SendTitles(IPlayer player) {
        var titles = player.Titles;

        var packet = new SpPlayerTitles {
            Titles = titles.ToArrayId()
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendTitle(IPlayer player, IInstance instance) {
        var players = instance.GetPlayers();
        var list = players.Select(p => p.GetConnection().Id);

        var packet = new PacketSelectedTitle() {
            Index = player.IndexOnInstance,
            Id = player.Character.TitleId
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendExperience(IPlayer player, int experience, int maximum) {
        var packet = new SpPlayerExperience() {
            Experience = experience,
            Maximum = maximum
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendAttributePoint(IPlayer player) {
        var packet = new SpAttributePoint() {
            Points = player.Character.Points
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendInventory(IPlayer player) {
        var maximum = player.Character.MaximumInventories;
        var inventories = player.Inventories.ToList();
        var count = inventories.Count;

        var packet = new SpPlayerInventory {
            MaximumInventories = maximum,
            Inventories = new DataInventory[count]
        };

        for (var i = 0; i < count; ++i) {
            var inventory = inventories[i];

            packet.Inventories[i] = new DataInventory() {
                Id = inventory.ItemId,
                Index = inventory.InventoryIndex,
                Value = inventory.Value,
                Level = inventory.Level,
                Bound = inventory.Bound,
                AttributeId = inventory.AttributeId,
                UpgradeId = inventory.UpgradeId
            };
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendInventoryUpdate(IPlayer player, int inventoryIndex) {
        var inventory = player.Inventories.FindByIndex(inventoryIndex);

        if (inventory is not null) {
            var packet = new SpPlayerInventoryUpdate() {
                Inventory = new DataInventory() {
                    Index = inventory.InventoryIndex,
                    Id = inventory.ItemId,
                    Value = inventory.Value,
                    Level = inventory.Level,
                    Bound = inventory.Bound,
                    AttributeId = inventory.AttributeId,
                    UpgradeId = inventory.UpgradeId
                }
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }
    }

    public void SendPlayerModel(IPlayer player, IInstance instance) {
        var players = instance.GetPlayers();
        var list = players.Select(p => p.GetConnection().Id);

        var model = player.Character.CostumeModel;

        model = model > 0 ? model : player.Character.Model;

        var packet = new SpPlayerModel() {
            Index = player.IndexOnInstance,
            Model = model
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendEquipment(IPlayer player) {
        var equipments = player.Equipments.ToList();
        var count = equipments.Count;

        var packet = new SpPlayerEquipment() {
            Equipments = new DataEquipment[count]
        };

        for (var i = 0; i < count; ++i) {
            var equipment = equipments[i];

            packet.Equipments[i] = new DataEquipment() {
                Id = equipment.ItemId,
                Index = equipment.InventoryIndex,
                Level = equipment.Level,
                Bound = equipment.Bound,
                AttributeId = equipment.AttributeId,
                UpgradeId = equipment.UpgradeId
            };
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendEquipmentUpdate(IPlayer player, PlayerEquipmentType index) {
        var equipment = player.Equipments.Get(index);

        if (equipment is not null) {
            var packet = new SpPlayerEquipmentUpdate() {
                Equipment = new DataEquipment() {
                    Index = index,
                    Id = equipment.ItemId,
                    Level = equipment.Level,
                    Bound = equipment.Bound,
                    AttributeId = equipment.AttributeId,
                    UpgradeId = equipment.UpgradeId
                }
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }
    }

    public void SendHeraldry(IPlayer player) {
        var heraldries = player.Heraldries.ToList();
        var count = heraldries.Count;

        var packet = new SpPlayerHeraldry() {
            Heraldries = new DataHeraldry[count]
        };

        for (var i = 0; i < count; ++i) {
            var heraldry = heraldries[i];

            packet.Heraldries[i] = new DataHeraldry() {
                Id = heraldry.ItemId,
                Index = heraldry.InventoryIndex,
                Level = heraldry.Level,
                AttributeId = heraldry.AttributeId,
                UpgradeId = heraldry.UpgradeId
            };
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendHeraldryUpdate(IPlayer player, int index) {
        var heraldry = player.Heraldries.Get(index);

        if (heraldry is not null) {
            var packet = new SpPlayerHeraldryUpdate() {
                Heraldry = new DataHeraldry() {
                    Index = index,
                    Id = heraldry.ItemId,
                    Level = heraldry.Level,
                    AttributeId = heraldry.AttributeId,
                    UpgradeId = heraldry.UpgradeId
                }
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }
    }

    public void SendWarehouse(IPlayer player) {
        var inventories = player.Warehouse.ToList();
        var count = inventories.Count;

        var packet = new SpPlayerWarehouse() {
            Inventories = new DataInventory[count]
        };

        for (var i = 0; i < count; ++i) {
            var inventory = inventories[i];

            packet.Inventories[i] = new DataInventory() {
                Id = inventory.ItemId,
                Index = inventory.InventoryIndex,
                Value = inventory.Value,
                Level = inventory.Level,
                Bound = inventory.Bound,
                AttributeId = inventory.AttributeId,
                UpgradeId = inventory.UpgradeId
            };
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendWarehouseUpdate(IPlayer player, int index) {
        var inventory = player.Warehouse.FindByIndex(index);

        if (inventory is not null) {
            var packet = new SpPlayerWarehouseUpdate() {
                Inventory = new DataInventory() {
                    Index = index,
                    Id = inventory.ItemId,
                    Value = inventory.Value,
                    Level = inventory.Level,
                    Bound = inventory.Bound,
                    AttributeId = inventory.AttributeId,
                    UpgradeId = inventory.UpgradeId
                }
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }
    }

    public void SendCraftData(IPlayer player) {
        var packet = new SpCraftData() {
            Type = player.Craft.Profession,
            Level = player.Craft.Level,
            Experience = player.Craft.Experience,
            Maximum = player.Craft.NextLevelExperience
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendCraftExperience(IPlayer player) {
        var packet = new SpCraftExperience() {
            Experience = player.Craft.Experience,
            Maximum = player.Craft.NextLevelExperience
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendRecipes(IPlayer player) {
        var packet = new SpRecipes() {
            Recipes = player.Recipes.ToArrayId()
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendAddRecipe(IPlayer player, int recipeId) {
        var packet = new SpAddRecipe() {
            Recipe = recipeId
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendStartCraft(IPlayer player, int step) {
        var packet = new SpCraftStart() {
            Step = step
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendQuickSlot(IPlayer player) {
        var quickSlots = player.QuickSlots.ToList();
        var count = quickSlots.Count;

        var packet = new SpQuickSlot() {
            QuickSlot = new DataQuickSlot[count]
        };

        for (var i = 0; i < count; ++i) {
            var quick = quickSlots[i];

            packet.QuickSlot[i] = new DataQuickSlot() {
                Index = quick.QuickSlotIndex,
                ObjectType = quick.ObjectType,
                ObjectValue = quick.ObjectValue
            };
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendQuickSlotUpdate(IPlayer player, int index) {
        var quick = player.QuickSlots.Get(index);

        if (quick is not null) {
            var packet = new SpQuickSlotUpdate() {
                QuickSlot = new DataQuickSlot() {
                    Index = index,
                    ObjectType = quick.ObjectType,
                    ObjectValue = quick.ObjectValue
                }
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }
    }

    public void SendCurrency(IPlayer player) {
        var currencies = player.Currencies.ToList();
        var count = currencies.Count;

        var packet = new SpCurrency() {
            Currencies = new DataCurrency[count]
        };

        for (var i = 0; i < count; i++) {
            packet.Currencies[i] = new DataCurrency() {
                Id = currencies[i].CurrencyId,
                Amount = currencies[i].CurrencyValue
            };
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendCurrencyUpdate(IPlayer player, CurrencyType id) {
        var currency = player.Currencies.GetCurrency(id);

        var packet = new SpCurrencyUpdate() {
            Currency = new DataCurrency() {
                Id = currency.CurrencyId,
                Amount = currency.CurrencyValue
            }
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendInstanceEntities(IPlayer player, IInstance instance) {
        var entities = instance.Entities;

        if (entities.Count > 0) {
            var packet = new SpInstanceEntities() {
                Entities = new DataEntity[entities.Count]
            };

            for (var i = 0; i < entities.Count; ++i) {
                var entity = entities[i];

                packet.Entities[i] = new DataEntity() {
                    Index = i,
                    Id = entity.Id,
                    X = entity.X,
                    Y = entity.Y,
                    Direction = entity.Direction,
                    IsDead = entity.IsDead,
                };

                packet.Entities[i].Vital = new int[] {
                        entity.Vitals.Get(Vital.HP),
                        entity.Vitals.Get(Vital.MP),
                        entity.Vitals.Get(Vital.Special)
                    };

                packet.Entities[i].MaximumVital = new int[] {
                        entity.Vitals.GetMaximum(Vital.HP),
                        entity.Vitals.GetMaximum(Vital.MP),
                        entity.Vitals.GetMaximum(Vital.Special)
                    };
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }
    }

    public void SendInstanceEntity(IInstance instance, int index) {
        var players = instance.GetPlayers();
        var list = players.Select(p => p.GetConnection().Id);

        var entity = instance.Entities[index];

        var packet = new SpInstanceEntity() {
            Entity = new DataEntity() {
                Index = index,
                Id = entity.Id,
                X = entity.X,
                Y = entity.Y,
                Direction = entity.Direction,
                IsDead = entity.IsDead,
                Vital = new int[] {
                        entity.Vitals.Get(Vital.HP),
                        entity.Vitals.Get(Vital.MP),
                        entity.Vitals.Get(Vital.Special)
                    },
                MaximumVital = new int[] {
                        entity.Vitals.GetMaximum(Vital.HP),
                        entity.Vitals.GetMaximum(Vital.MP),
                        entity.Vitals.GetMaximum(Vital.Special)
                    }
            }
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendInstanceEntityVital(IInstance instance, int index) {
        var players = instance.GetPlayers();
        var list = players.Select(p => p.GetConnection().Id);

        var entity = instance.Entities[index];

        var packet = new SpInstanceEntityVital() {
            Index = index,
            Vital = new int[] {
                        entity.Vitals.Get(Vital.HP),
                        entity.Vitals.Get(Vital.MP),
                        entity.Vitals.Get(Vital.Special)
                    },
            MaximumVital = new int[] {
                        entity.Vitals.GetMaximum(Vital.HP),
                        entity.Vitals.GetMaximum(Vital.MP),
                        entity.Vitals.GetMaximum(Vital.Special)
                    }
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendInstanceEntityMove(IInstance instance, MovementType movementType, int index) {
        var players = instance.GetPlayers();
        var list = players.Select(p => p.GetConnection().Id);

        var entity = instance.Entities[index];

        var packet = new SpInstanceEntityMove() {
            Index = index,
            X = entity.X,
            Y = entity.Y,
            Direction = entity.Direction,
            MovementType = movementType
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendSkills(IPlayer player) {
        var inventories = player.Skills.ToList();

        var packet = new SpSkill() {
            Skills = new DataSkill[inventories.Count]
        };

        for (var i = 0; i < inventories.Count; ++i) {
            packet.Skills[i].Id = inventories[i].SkillId;
            packet.Skills[i].Level = inventories[i].SkillLevel;
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendSkillUpdate(IPlayer player, CharacterSkill skill) {
        var packet = new SpSkillUpdate() {
            Skill = new DataSkill() {
                Id = skill.SkillId,
                Level = skill.SkillLevel
            }
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendPassives(IPlayer player) {
        var inventories = player.Passives.ToList();

        var packet = new SpPassive() {
            Passives = new DataSkill[inventories.Count]
        };

        for (var i = 0; i < inventories.Count; ++i) {
            packet.Passives[i].Id = inventories[i].PassiveId;
            packet.Passives[i].Level = inventories[i].PassiveLevel;
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendPassiveUpdate(IPlayer player, CharacterPassive passive) {
        var packet = new SpPassiveUpdate() {
            Passive = new DataSkill() {
                Id = passive.PassiveId,
                Level = passive.PassiveLevel
            }
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendPremiumService(IPlayer player, Premium premium, string ending) {
        const int Multiplier = 100;

        var packet = new SpPremiumService() {
            Id = premium.Id,
            Name = premium.Name,
            EndDate = ending,
            Rates = new DataRate() {
                Character = Convert.ToInt32(premium.Character * Multiplier),
                Party = Convert.ToInt32(premium.Party * Multiplier),
                Guild = Convert.ToInt32(premium.Guild * Multiplier),
                Skill = Convert.ToInt32(premium.Skill * Multiplier),
                Craft = Convert.ToInt32(premium.Craft * Multiplier),
                Quest = Convert.ToInt32(premium.Quest * Multiplier),
                Pet = Convert.ToInt32(premium.Pet * Multiplier),
                GoldChance = Convert.ToInt32(premium.GoldChance * Multiplier),
                GoldIncrease = Convert.ToInt32(premium.GoldIncrease * Multiplier),
                ItemDrop = new int[] {
                        Convert.ToInt32(premium.ItemDrops[Rarity.Common] * Multiplier),
                        Convert.ToInt32(premium.ItemDrops[Rarity.Uncommon] * Multiplier),
                        Convert.ToInt32(premium.ItemDrops[Rarity.Rare] * Multiplier),
                        Convert.ToInt32(premium.ItemDrops[Rarity.Epic] * Multiplier),
                        Convert.ToInt32(premium.ItemDrops[Rarity.Mythic] * Multiplier),
                        Convert.ToInt32(premium.ItemDrops[Rarity.Ancient] * Multiplier),
                        Convert.ToInt32(premium.ItemDrops[Rarity.Legendary] * Multiplier),
                        Convert.ToInt32(premium.ItemDrops[Rarity.Ethereal] * Multiplier),
                    }
            }
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendUpgradeData(IPlayer player, int inventoryIndex) {
        var packet = new SpUpgradeData() {
            SelectedInventoryIndex = inventoryIndex
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendUpgradeData(IPlayer player, int inventoryIndex, UpgradeLevel data) {
        var packet = new SpUpgradeData() {
            Break = data.Break,
            Success = data.Success,
            Reduce = data.Reduce,
            Cost = data.Cost,
            SelectedInventoryIndex = inventoryIndex
        };

        var count = data.Requirements.Count;

        packet.Requirements = new DataRequirement[count];

        for (var i = 0; i < count; ++i) {
            packet.Requirements[i] = new DataRequirement() {
                Id = data.Requirements[i].Id,
                Amount = data.Requirements[i].Amount
            };
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendTarget(IPlayer player, TargetType targetType, int index) {
        var packet = new PacketTarget() {
            Index = index,
            TargetType = targetType
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendConversation(IPlayer player, int npcId) {
        var packet = new SpConversation() {
            NpcId = npcId
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendConversationOption(IPlayer player, int id, int chatIndex) {
        var packet = new PacketConversationOption() {
            ConversationId = id,
            ChatIndex = chatIndex
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendConversationClose(IPlayer player) {
        var packet = new SpConversationClose();

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendWarehouseOpen(IPlayer player) {
        var packet = new SpWarehouseOpen();

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendUpgradeOpen(IPlayer player) {
        var packet = new SpUpgradeOpen();

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendCraftOpen(IPlayer player) {
        var packet = new SpCraftOpen();

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendShopOpen(IPlayer player, Shop shop) {
        var items = shop.Items;

        var packet = new SpShopOpen() {
            Name = shop.Name,
            Items = new DataShopItem[items.Count]
        };

        for (var i = 0; i < items.Count; ++i) {
            packet.Items[i].Id = items[i].Id;
            packet.Items[i].Value = items[i].Value;
            packet.Items[i].Level = items[i].Level;
            packet.Items[i].Bound = items[i].Bound;
            packet.Items[i].AttributeId = items[i].AttributeId;
            packet.Items[i].UpgradeId = items[i].UpgradeId;
            packet.Items[i].CurrencyId = items[i].CurrencyId;
            packet.Items[i].Price = items[i].Price;
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendViewEquipment(IPlayer player, IPlayer source) {
        var equipments = source.Equipments.ToList();
        var count = equipments.Count;

        var packet = new SpViewEquipment() {
            Name = source.Character.Name,
            Level = source.Character.Level,
            ClassCode = source.Character.ClassCode,
            Equipments = new DataEquipment[count]
        };

        for (var i = 0; i < count; ++i) {
            var equipment = equipments[i];

            packet.Equipments[i] = new DataEquipment() {
                Id = equipment.ItemId,
                Index = equipment.InventoryIndex,
                Level = equipment.Level,
                Bound = equipment.Bound,
                AttributeId = equipment.AttributeId,
                UpgradeId = equipment.UpgradeId
            };
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendSettings(IPlayer player) {
        var packet = new SpSettings() {
            ViewEquipment = player.Settings.ViewEquipment
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendAnimation(IInstance instance, int animation, int x, int y, TargetType lockType, int lockIndex, bool isCasting) {
        var list = instance.GetPlayers().Select(p => p.GetConnection().Id);

        var packet = new SpAnimation() {
            Animation = animation,
            X = x, 
            Y = y, 
            LockType = lockType, 
            LockIndex = lockIndex,
            IsCasting = isCasting
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendCancelAnimation(IInstance instance, int lockIndex) {
        var list = instance.GetPlayers().Select(p => p.GetConnection().Id);

        var packet = new SpCancelAnimation() {
            Index = lockIndex
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendSkillCooldown(IPlayer player, int index) {
        var msg = Writer!.CreateMessage(new SpSkillCooldown() { Index = index});

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendClearCast(IPlayer player) {
        var msg = Writer!.CreateMessage(new SpClearCast());

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }


    public void SendChest(IInstance instance, IInstanceChest chest) {
        var list = instance.GetPlayers().Select(p => p.GetConnection().Id);

        var packet = new SpChests {
            Chests = new DataChest[1]
        };

        packet.Chests[0].Index = chest.Index;
        packet.Chests[0].X = chest.X;
        packet.Chests[0].Y = chest.Y;
        packet.Chests[0].Sprite = chest.Chest.Sprite;
 
        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendChests(IInstance instance) {

    }


    private SpPlayerData CreatePlayerDataPacket(IPlayer player) {
        var character = player.Character;
        var model = character.CostumeModel;

        model = model > 0 ? model : character.Model;

        return new SpPlayerData() {
            Index = player.IndexOnInstance,
            AccountLevel = player.AccountLevel,
            Name = character.Name,
            ClassCode = character.ClassCode,
            Level = character.Level,
            MapId = character.Map,
            X = (short)character.X,
            Y = (short)character.Y,
            Direction = character.Direction,
            TitleId = character.TitleId,
            IsDead = character.IsDead,
            Model = model
        };
    }

    private SpPlayerHp CreatePlayerHpPacket(IPlayer player) {
        var vitals = player.Vitals;

        return new SpPlayerHp() {
            Index = player.IndexOnInstance,
            MaximumHp = vitals.GetMaximum(Vital.HP),
            Hp = vitals.Get(Vital.HP)
        };
    }

    private SpPlayerMp CreatePlayerMpPacket(IPlayer player) {
        var vitals = player.Vitals;

        return new SpPlayerMp() {
            Index = player.IndexOnInstance,
            MaximumMp = vitals.GetMaximum(Vital.MP),
            Mp = vitals.Get(Vital.MP)
        };
    }

}