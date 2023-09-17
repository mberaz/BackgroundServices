﻿using BackgroundService.Implementation;
using BackgroundServices.BackgroundModels.TasksSettings;

namespace BackgroundServices.BackgroundModels
{
    //https://wildermuth.com/2022/05/04/using-background-services-in-asp-net-core/
    public class MainBackgroundTaskService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly TasksToRun _tasks;
        private readonly IMonitorService _monitorService;
        private readonly IImportingService _importingService;
        private Task? _currentTask;

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
                if (taskToRun?.ImportingSettings != null)
                {
                    _currentTask = ExecuteImportingTask(taskToRun.ImportingSettings);
                    await _currentTask;
                }
                else if (taskToRun?.MonitorSettings != null)
                {
                    _currentTask = ExecuteMonitorTask(taskToRun.MonitorSettings);
                    await _currentTask;
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
    }
}