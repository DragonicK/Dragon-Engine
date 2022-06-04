namespace Dragon.Network;

public interface IIndexGenerator {
    int GetNextIndex();
    void Remove(int index);
}