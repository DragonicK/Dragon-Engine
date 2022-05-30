namespace Crystalshire.Model.Models;

public class Directions {
    public int Id { get; set; }
    public string Name {
        get {
            return _name;
        }

        set {
            _name = value;

            Up.Name = $"{_name} Up";
            Down.Name = $"{_name} Down";
            Left.Name = $"{_name} Left";
            Right.Name = $"{_name} Right";
        }
    }
    public Movement Up { get; set; }
    public Movement Down { get; set; }
    public Movement Left { get; set; }
    public Movement Right { get; set; }

    private string _name;

    public Directions() {
        _name = "無名";
        Up = new Movement() { Name = $"{Name} Up" };
        Down = new Movement() { Name = $"{Name} Down" };
        Left = new Movement() { Name = $"{Name} Left" };
        Right = new Movement() { Name = $"{Name} Right" };
    }
}