using Dragon.Core.Content;
using Dragon.Core.Model.Maps;
using Dragon.Core.Serialization;

using Dragon.Game.Regions;
using Dragon.Game.Network;
using Dragon.Game.Configurations;

namespace Dragon.Game.Instances;

public sealed class InstanceLoader {
    public IDictionary<int, IInstance> Instances { get; private set; }

    private readonly IConfiguration _configuration;
    private readonly IDatabase<IMap> _maps;
    private readonly IPacketSender _sender;
    private readonly string _folder;

    private int processed;

    public InstanceLoader(string folder, IConfiguration configuration, IDatabase<IMap> maps, IPacketSender sender) {
        _maps = maps;
        _folder = folder;
        _sender = sender;
        _configuration = configuration;

        Instances = new Dictionary<int, IInstance>();
    }

    public void LoadInstances() {
        processed = 0;

        if (Directory.Exists(_folder)) {
            ProcessFields(_folder);

            if (processed == 0) {
                SaveExample();
            }
        }
    }

    private void ProcessFields(string root) {
        var files = GetFiles(root);

        if (files?.Length > 0) {
            foreach (var file in files) {
                var instance = GetInstanceRegion(file);

                if (instance is not null) {
                    processed++;
                    AddField(instance);
                }
            }
        }

        var folders = GetFolders(root);

        if (folders?.Length > 0) {
            foreach (var folder in folders) {
                ProcessFields(folder);
            }
        }
    }

    private void AddField(Region region) {
        if (region is not null) {
            if (!Instances.ContainsKey(region.Id)) {
                var map = _maps[region.MapId];

                if (map is not null) {
                    var field = new Field(map, region, _configuration, _sender);

                    Instances.Add(field.Id, field);
                }
            }
        }
    }

    private string[]? GetFolders(string root) {
        return Directory.GetDirectories(root);
    }

    private string[]? GetFiles(string folder) {
        if (Directory.Exists(folder)) {
            return Directory.GetFiles(folder);
        }

        return null;
    }

    private Region? GetInstanceRegion(string file) {
        Region? region = null;

        if (Json.FileExists(file)) {
            region = Json.Get<Region>(file);

            if (region is not null) {
                Json.Save(file, region);
            }
        }

        return region;
    }

    private void SaveExample() {
        var file = $"{_folder}/default.json";

        var region = new Region();
        region.Entities.Add(new RegionEntity());
        region.Entities.Add(new RegionEntity());
        region.Entities.Add(new RegionEntity());

        Json.Save(file, region);
    }
}