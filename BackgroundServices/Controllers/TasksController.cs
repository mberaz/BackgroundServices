using BackgroundService.Implementation.Settings;
using BackgroundServices.BackgroundModels;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TasksToRun _tasksToRun;

        public TasksController(TasksToRun tasksToRun)
        {
            _tasksToRun = tasksToRun;
        }

        [HttpGet("monitor")]
        public Task<string> Monitor()
        {
            _tasksToRun.Enqueue(TaskSettings.FromMonitorSettings(new MonitorSettings
            {
                ApiKey = "aaa"
            }));
            return Task.FromResult("monitoring");
        }

        [HttpGet("import")]
        public Task<string> Import()
        {
            _tasksToRun.Enqueue(TaskSettings.FromImporterSettings(new ImportingSettings
            {
                Source = "http://data.com",
                Count = 100
            }));
            return Task.FromResult("importing");
        }
    }
}