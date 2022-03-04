public interface ITaskCallback
{
    void OnEnterRange(AbstractTask task);
    void OnExitRange(AbstractTask task);
    void OnClosedTask(AbstractTask task);
    void OnOpenTask(AbstractTask task);
    void OnCompleteTask(AbstractTask task);
}
