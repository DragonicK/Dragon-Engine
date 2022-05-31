namespace Crystalshire.Core.Model;

public class Experience {
    public int MaximumLevel { get; set; }
    public Dictionary<int, int> Experiences { get; set; }

    public Experience() {
        MaximumLevel = 20;
        Experiences = new Dictionary<int, int>();

        for (var i = 1; i <= MaximumLevel; ++i) {
            Experiences.Add(i, (i * 800) + (i ^ 5) + ((short.MaxValue / 20) * i));
        }
    }

    public int Get(int level) {
        if (Experiences.ContainsKey(level)) {
            return Experiences[level];
        }

        return 0;
    }
}