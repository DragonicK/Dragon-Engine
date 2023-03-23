namespace Dragon.Core.Model.Npcs;

public class Npc {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Sound { get; set; }
    public NpcBehaviour Behaviour { get; set; }
    public int ModelId { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
    public int AttributeId { get; set; }
    public string Greetings { get; set; }
    public IList<int> Conversations { get; set; }

    public Npc() {
        Name = string.Empty;
        Title = string.Empty;
        Sound = "None.";
        Greetings = string.Empty;

        Conversations = new List<int>();
    }

    public override string ToString() {
        return Name;
    }
}