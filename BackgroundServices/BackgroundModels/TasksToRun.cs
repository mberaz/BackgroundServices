using System.Collections.Concurrent;

namespace BackgroundServices.BackgroundModels;

public class TasksToRun
{
    private readonly ConcurrentQueue<TaskSettings> _tasks = new();

    public TasksToRun() => _tasks = new ConcurrentQueue<TaskSettings>();

    public void Enqueue(TaskSettings settings) => _tasks.Enqueue(settings);

    public TaskSettings? Dequeue()
    {
        var hasTasks = _tasks.TryDequeue(out var settings);
        return hasTasks ? settings : null;
    }
}