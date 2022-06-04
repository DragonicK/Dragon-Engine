namespace Dragon.Core.Services;

public interface IService {
    ServicePriority Priority { get; }
    void Start();
    void Stop();
}