
public interface ICommand<T> {
    void Execute();
    void Undo();
    void Stop();

    void Update();
}

