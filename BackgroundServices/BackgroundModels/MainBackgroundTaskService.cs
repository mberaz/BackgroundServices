using BackgroundService.Implementation;
using BackgroundService.Implementation.Settings;

namespace BackgroundServices.BackgroundModels
{
    public class MainBackgroundTaskService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly TasksToRun _tasks;
        private readonly IMonitorService _monitorService;
        private readonly IImportingService _importingService;

        private readonly TimeSpan _timeSpan = TimeSpan.FromSeconds(1);//get number of seconds from config
        public MainBackgroundTaskService(TasksToRun tasks,
            IMonitorService monitorService,
            IImportingService importingService)
        {
            _tasks = tasks;
            _monitorService = monitorService;
            _importingService = importingService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new(_timeSpan);
            while (!stoppingToken.IsCancellationRequested &&
                   await timer.WaitForNextTickAsync(stoppingToken))
            {
                var taskToRun = _tasks.Dequeue();
                var currentTask = Task.CompletedTask;

                if (taskToRun?.ImportingSettings != null)
                {
                    currentTask = ExecuteImportingTask(taskToRun.ImportingSettings);
                }
                else if (taskToRun?.MonitorSettings != null)
                {
                    currentTask = ExecuteMonitorTask(taskToRun.MonitorSettings);
                }

                await currentTask;//await completion to make the task run one at a time
            }
        }

        public Task ExecuteMonitorTask(MonitorSettings monitorSettings) =>
            _monitorService.Monitor(monitorSettings);

        public Task ExecuteImportingTask(ImportingSettings importingSettings) =>
            _importingService.Import(importingSettings);
    }
}
