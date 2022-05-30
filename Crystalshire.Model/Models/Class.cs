namespace Crystalshire.Model.Models;

public class Class {
    public int Id { get; set; }
    public string Name { get; set; }
    public Directions Attack { get; set; }
    public Directions Death { get; set; }
    public Directions Ressurrection { get; set; }
    public Directions Talk { get; set; }
    public Directions Gathering { get; set; }
    public Directions Walking { get; set; }
    public Directions Running { get; set; }
    public Directions Idle { get; set; }

    public IList<Directions> Specials { get; set; }
    public IList<Directions> Emotes { get; set; }

    public Class() {
        Name = string.Empty;

        Attack = new Directions() { Name = "Attack" };
        Death = new Directions() { Name = "Death" };
        Ressurrection = new Directions() { Name = "Ressurrection" };
        Talk = new Directions() { Name = "Talk" };
        Gathering = new Directions() { Name = "Gathering" };
        Walking = new Directions() { Name = "Walking" };
        Running = new Directions() { Name = "Running" };
        Idle = new Directions() { Name = "Idle" };

        Specials = new List<Directions>();
        Emotes = new List<Directions>();
    }
}