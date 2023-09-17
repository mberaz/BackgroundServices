namespace BackgroundService.Implementation
{
    public class MonitorService:IMonitorService
    {
        public Task Monitor(string apiKey)
        {
            return Task.FromResult("");
        }
    }
}
