using Crystalshire.Core.Model.Npcs;

namespace Crystalshire.Core.Content {
    public class Npcs : Database<Npc> {

        public override void Load() {
            var path = $"{Folder}/{FileName}";

            if (!File.Exists(path)) {
                return;
            }

            using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(file);
            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++) {
                var npc = new Npc() {
                    Id = reader.ReadInt32(),
                    Name = reader.ReadString(),
                    Description = reader.ReadString(),
                    Sound = reader.ReadString(),
                    Behaviour = (NpcBehaviour)reader.ReadInt32(),
                    ModelId = reader.ReadInt32(),
                    Level = reader.ReadInt32(),
                    AttributeId = reader.ReadInt32(),
                    Experience = reader.ReadInt32(),
                    Greetings = reader.ReadString()
                };

                var conversationCount = reader.ReadInt32();

                for (var x = 0; x < conversationCount; ++x) {
                    npc.Conversations.Add(reader.ReadInt32());
                }

                values.Add(npc.Id, npc);
            }
        }

        public override void Save() {
            var path = $"{Folder}/{FileName}";

            using var file = new FileStream(path, FileMode.Create, FileAccess.Write);
            using var writer = new BinaryWriter(file);

            var count = values.Count;

            writer.Write(count);

            var ordered = values.Select(p => p.Value).OrderBy(p => p.Id).ToList();

            for (var i = 0; i < ordered.Count; ++i) {
                writer.Write(ordered[i].Id);
                writer.Write(ordered[i].Name);
                writer.Write(ordered[i].Description);
                writer.Write(ordered[i].Sound);
                writer.Write((int)ordered[i].Behaviour);
                writer.Write(ordered[i].ModelId);
                writer.Write(ordered[i].Level);
                writer.Write(ordered[i].AttributeId);
                writer.Write(ordered[i].Experience);
                writer.Write(ordered[i].Greetings);

                writer.Write(ordered[i].Conversations.Count);

                for (var x = 0; x < ordered[i].Conversations.Count; ++x) {
                    writer.Write(ordered[i].Conversations[x]);
                }
            }
        }

    }
}