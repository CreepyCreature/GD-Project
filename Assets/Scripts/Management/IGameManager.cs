public interface IGameManager
{
    ManagerStatus status { get; }

    void Initialize();
}