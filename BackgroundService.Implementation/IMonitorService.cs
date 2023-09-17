namespace BackgroundService.Implementation
{
    public interface IMonitorService
    {
        Task Monitor(string apiKey);
    }
}
