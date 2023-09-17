using System.Collections.Concurrent;

namespace BackgroundServices.BackgroundModels;

public class TasksToRun
{
    private readonly ConcurrentQueue<HostingSettings> _tasks = new();

    public TasksToRun() => _tasks = new ConcurrentQueue<HostingSettings>();

    public void Enqueue(HostingSettings settings) => _tasks.Enqueue(settings);

    public HostingSettings? Dequeue()
    {
        var hasTasks = _tasks.TryDequeue(out var settings);
        return hasTasks ? settings : null;
    }
}