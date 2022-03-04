public interface ITaskCallback
{
    void OnEnterRange(AbstractTask task);
    void OnExitRange(AbstractTask task);
    void OnClosedTask(AbstractTask task);
}
