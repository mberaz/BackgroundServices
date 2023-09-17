using BackgroundService.Implementation;
using BackgroundServices.HostedServices.TasksSettings;

namespace BackgroundServices.HostedServices
{
    public class MainHostedService : IHostedService
    {
        private readonly TasksToRun _tasks;
        private readonly IMonitorService _monitorService;
        private readonly IImportingService _importingService;

        private CancellationTokenSource _tokenSource;

        private Task? _currentTask;
        public MainHostedService(TasksToRun tasks,
            IMonitorService monitorService,
            IImportingService importingService)
        {
            _tasks = tasks;
            _monitorService = monitorService;
            _importingService = importingService;
            //var locator = new ServiceLocator();
            //_importingService = locator.SeriesEpisodesService();
            //_monitorService = locator.ApiMonitorService();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            while (cancellationToken.IsCancellationRequested == false)
            {
                try
                {
                    //var taskToRun = _tasks.Dequeue(_tokenSource.Token);
                    //if (taskToRun.ImportingSettings != null)
                    //{
                    //    _currentTask = ExecuteImportingTask(taskToRun.ImportingSettings);
                    //    await _currentTask;
                    //}
                    //else if (taskToRun.MonitorSettings != null)
                    //{
                    //    _currentTask = ExecuteMonitorTask(taskToRun.MonitorSettings);
                    //    await _currentTask;
                    //}
                }
                catch (OperationCanceledException e)
                {
                    Console.WriteLine();
                    // execution cancelled
                }
            }
        }

        public async Task ExecuteMonitorTask(MonitorSettings monitorSettings)
        {
            await _monitorService.Monitor(monitorSettings.ApiKey);
        }

        public async Task ExecuteImportingTask(ImportingSettings settings)
        {
            await _importingService.Import(settings.Source, settings.Count);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _tokenSource.Cancel(); // cancel "waiting" for task in blocking collection

            if (_currentTask == null) return;

            // wait when _currentTask is complete
            await Task.WhenAny(_currentTask, Task.Delay(-1, cancellationToken));
        }
    }
}
