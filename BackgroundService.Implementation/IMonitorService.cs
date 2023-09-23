using BackgroundService.Implementation.Settings;

namespace BackgroundService.Implementation
{
    public interface IMonitorService
    {
        Task Monitor(MonitorSettings settings);
    }
}
