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
                if (taskToRun == null) continue;
                //call the relevant service based on what 
                // settings object in not NULL
                if (taskToRun?.ImportingSettings != null)
                {
                    await ExecuteImportingTask(taskToRun.ImportingSettings);
                }

                if (taskToRun?.MonitorSettings != null)
                {
                    await ExecuteMonitorTask(taskToRun.MonitorSettings);
                }
            }
        }

        public Task ExecuteMonitorTask(MonitorSettings monitorSettings) =>
            _monitorService.Monitor(monitorSettings);

        public Task ExecuteImportingTask(ImportingSettings importingSettings) =>
            _importingService.Import(importingSettings);
    }
}
