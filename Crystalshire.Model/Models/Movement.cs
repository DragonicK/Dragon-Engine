namespace Crystalshire.Model.Models;

public class Movement {
    public Frame? this[int index] {
        get {
            if ((Count == 0) || (index < 0 || index >= Count)) {
                return null;
            }

            return frames[index];
        }

        set {
            if ((Count == 0) || (index < 0 || index >= Count)) {
                return;
            }

            frames[index] = value!;
        }
    }
    public int Count => frames.Count;
    public string Name { get; set; }
    public int Time { get; set; } = 60;
    public bool Continuously { get; set; }
    public bool WaitResponse { get; set; }

    private readonly IList<Frame> frames;

    public Movement() {
        Name = "無名";
        frames = new List<Frame>();
    }

    public void Add(Frame frame) {
        frames.Add(frame);
    }

    public void Clear() {
        frames.Clear();
    }

    public void Remove(Frame frame) {
        frames.Remove(frame);
    }

    public void Remove(int index) {
        if ((Count == 0) || (index < 0 || index >= Count)) {
            return;
        }

        frames.RemoveAt(index);
    }

    public void RemoveLast() {
        if (frames.Count > 0) {
            var index = frames.Count - 1;
            frames.RemoveAt(index);
        }
    }

    public void SwapFrames(int oldIndex, int newIndex) {
        var copy = frames[oldIndex].Clone();

        frames[oldIndex] = frames[newIndex].Clone();
        frames[newIndex] = copy;
    }
}