using System.Collections.Concurrent;

namespace BackgroundServices.HostedServices;

public class TasksToRun
{
    private readonly BlockingCollection<HostingSettings> _tasks;

    public TasksToRun() => _tasks = new BlockingCollection<HostingSettings>();

    public void Enqueue(HostingSettings settings) => _tasks.Add(settings);

    public HostingSettings Dequeue(CancellationToken token) => _tasks.Take(token);
}