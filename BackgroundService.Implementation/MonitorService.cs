using BackgroundService.Implementation.Settings;

namespace BackgroundService.Implementation
{
    public class MonitorService:IMonitorService
    {
        public Task Monitor(MonitorSettings settings)
        {
            return Task.FromResult("");
        }
    }
}
